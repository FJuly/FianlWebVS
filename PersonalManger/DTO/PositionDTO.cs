using MODEL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalManger.DTO
{
    /// <summary>
    /// 浏览职位时前后台页面交互时需要的对象
    /// </summary>
   public class PositionDTO
    {
       /// <summary>
       /// 职位名称
       /// </summary>
       public string Postion;

       /// <summary>
       /// 担任相应职位人员
       /// </summary>
       public List<MemberInformationDTO> List;
    }
}
