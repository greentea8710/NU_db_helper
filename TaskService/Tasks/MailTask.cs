using Common;
using DistributedLock;
using Repository.Database;
using System.Net;
using System.Net.Mail;
using TaskService.Libraries.QueueTask;
using TaskService.Libraries.ScheduleTask;

namespace TaskService.Tasks
{
    public class MailTask(IServiceProvider serviceProvider, ILogger<MailTask> logger, IDHelper idHelper): BackgroundService
    {
        private readonly ILogger logger = logger;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ScheduleTaskBuilder.Builder(this);
            QueueTaskBuilder.Builder(this);

            CreateData();

            await Task.Delay(-1, stoppingToken);
        }
        [ScheduleTask(Name = "MT.ShowTime", Cron = "0/1 * * * * ?")]
        public void ShowTime()
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var distLock = scope.ServiceProvider.GetRequiredService<IDistributedLock>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "MailTask.WriteHello");
            }
        }


        //[ScheduleTask(Name = "MT.CreateData", Cron = "0/1 * * * * ?")]
        public void CreateData()
        {
            
            try {

                idHelper.GetId();
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();


                try
                {
                    TMail mail = new()
                    {
                        Id = idHelper.GetId(),
                        MailId = idHelper.GetId(),
                        Content = "待會要執行代辦事項",
                        SenderEmail = "greentea8710@gmail.com",
                        ReceiverEmail = "teapudding8710@gmail.com",
                        Subject = "測試郵件",
                        Time = DateTimeOffset.UtcNow,
                        Success = false
                    };
                    db.TMail.Add(mail);
                    db.SaveChanges();

                    TPeople people = new();
                    people.Id = idHelper.GetId();
                    people.Name = "呂璇";
                    people.Account = "teapudding8710@gmail.com";
                    db.TPeople.Add(people);
                    db.SaveChanges();


                    string Account = "greentea8710@gmail.com";
                    string Password = "vwfw zawo cjhb ommo";
                    SmtpClient client = new()
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(Account, Password),
                        EnableSsl = true
                    };

                    MailMessage message = new(mail.SenderEmail, mail.ReceiverEmail, mail.Subject, mail.Content);
                    client.Send(message);
                    DateTime sentTime = DateTime.Now;
                    Console.WriteLine("Email sent successfully at: " + sentTime);

                    // 更新資料庫中的 success 欄位為 true
                    mail.Success = true;

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "DemoTask.CreateData");
                    Console.WriteLine("Error sending email: " + ex.Message);
                    // 更新資料庫中的 success 欄位為 false
                }    
            }


            catch (Exception ex)
            {
                logger.LogError(ex, "MailTask.CreateData");
            }
        }
    }
}
