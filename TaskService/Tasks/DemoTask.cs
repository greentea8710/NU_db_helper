using Common;
using DistributedLock;
using NPOI.Util;
using Repository.Database;
using System.Globalization;
using TaskService.Libraries.QueueTask;
using TaskService.Libraries.ScheduleTask;
using Microsoft.EntityFrameworkCore;
using TaskService.Models.DemoTask;

namespace TaskService.Tasks
{
    public class DemoTask(IServiceProvider serviceProvider, ILogger<DemoTask> logger, IDHelper idHelper) : BackgroundService
    {
        private readonly ILogger logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ScheduleTaskBuilder.Builder(this);
            QueueTaskBuilder.Builder(this);

            await Task.Delay(-1, stoppingToken);
        }



        [ScheduleTask(Name = "DT.ShowTime", Cron = "0/1 * * * * ?")]
        public void ShowTime()
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                var distLock = scope.ServiceProvider.GetRequiredService<IDistributedLock>();

                Console.WriteLine(DateTime.Now);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DemoTask.WriteHello");
            }
        }
        [ScheduleTask(Name = "DT.CreateData", Cron = "0/1 * * * * ?")]
        public void CreateData()
        {
            try
            {
                idHelper.GetId();

                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                var student = db.TStudent.Where(t => t.Id == 142325926967902208).Include(t => t.TScore).FirstOrDefault();


                var student1 = db.TStudent.Where(t => t.Id == 142325926967902208).Select(t => new
                {
                    t.Name,
                    t.Phone,
                    ScoreInfos = t.TScore.Select(s => new
                    {
                        s.Id,
                        s.Score,
                        s.ScoreDate,
                        SubjectName = s.Subject.Name,
                        SubjectTeacher = s.Subject.Teacher
                    })
                }).FirstOrDefault();

                if (student1 != null)
                {

                    string studentJson = JsonHelper.ObjectToJson(student1);

                    Console.WriteLine(studentJson);
                    logger.LogInformation(studentJson);
                }



                var classInfo = db.TClass.Where(t => t.Id == 138305063465517056).FirstOrDefault();

                if (classInfo != null)
                {

                    classInfo.Grade = 6;
                    db.SaveChanges();
                }

        //TClass classInfo = new();
        //classInfo.Id = idHelper.GetId();
        //classInfo.Number = 1;
        //classInfo.Grade = 1;

        //db.TClass.Add(classInfo);
        //db.SaveChanges();

        //TStudent student = new();
        //student.Id = idHelper.GetId();
        //student.Name = "小名";
        //student.Number = 1;
        //student.Gender = true;
        //student.Phone = "123456";
        //student.ClassId = 138305063465517056;

        //db.TStudent.Add(student);
        //db.SaveChanges();


        //TSubject subject = new();
        //subject.Id = idHelper.GetId();
        //subject.Name = "英文";
        //subject.Teacher = "小美";

        //db.TSubject.Add(subject);
        ////db.SaveChanges();


        //TScore score = new();
        //score.Id = idHelper.GetId();
        //score.Score = decimal.Parse("90");
        //score.ScoreDate = new DateOnly(2024,02,25);
        //score.StudentId = student.Id;
        //score.SubjectId = subject.Id;

        //db.TScore.Add(score);
        //db.SaveChanges();

        /*
        TUseraccount useraccount = new();
        useraccount.Id = idHelper.GetId();
        useraccount.Account = "greentea8710";
        useraccount.Password = "password";
        db.TUseraccount.Add(useraccount);
        db.SaveChanges();
        */

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DemoTask.CreateData");
            }
        }


        [QueueTask(Name = "ShowName", Semaphore = 1, Duration = 5)]
        public void ShowName(string name)
        {
            Console.WriteLine(DateTime.Now + "姓名：" + name);
        }


        [ScheduleTask(Name = "S20240310", Cron = "0/1 * * * * ?")]
        public void S20240310()
        {


            try
            {
                idHelper.GetId();

                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();


                var info = db.TStudent.OrderBy(t => t.Number).Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Phone,
                    t.ClassId,
                    ClassNumber = t.Class.Number,
                    ClassGrade = t.Class.Grade,
                    ScoreList = t.TScore.OrderBy(s => s.ScoreDate).Select(s => new
                    {
                        s.Id,
                        s.Score,
                        s.ScoreDate,
                        s.Subject.Name,
                        StudentName = t.Name
                    }).ToList()
                }).ToList();


                var xinfo = db.TClass.OrderBy(t => t.Grade).Select(t => new DtoClassScore
                {

                    ClassId = t.Id,
                    ClassName = t.Grade.ToString() + "年级",
                    Students = t.TStudent.OrderBy(s => s.Number).Select(s => new DtoStudent
                    {
                        StudentId = s.Id.ToString(),
                        Name = s.Name,
                        Gender = s.Gender.ToString(),
                        Phone = s.Phone??"无手机号",
                        ScoreInfos = s.TScore.OrderBy(sc=>sc.Subject.Name).Select(sc => new DtoStudent.ScoreInfo
                        { 
                            Id = sc.Id,
                            ScoreDate = sc.ScoreDate,
                            SubjectName = sc.Subject.Name,
                            Score = sc.Score
                        }).ToList(),
                    }).ToList(),
                }).ToList();



               // var jsonData = JsonHelper.ObjectToJson(info);
                var jsonData = JsonHelper.ObjectToJson(xinfo);

                logger.LogInformation(jsonData);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DemoTask.S20240310");
            }
        }



    }
}
