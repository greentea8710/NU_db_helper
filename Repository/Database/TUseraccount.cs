using Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Database
{
  /// <summary>
  /// 登入人員表
  /// </summary>
  public class TUseraccount : CD
  {
    /// <summary>
    /// 帳號
    /// </summary>
    public string Account {  get; set; }

    /// <summary>
    /// 密碼
    /// </summary>
    public string Password { get; set; }
  }
}
