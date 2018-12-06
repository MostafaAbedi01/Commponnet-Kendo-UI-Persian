using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace LoyalBankWebUi.Areas.Usr.Models
{
    public class TestVm
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "شماره")]
        [UIHint("Number")]
        public int NumberTest { get; set; }

        [Display(Name = "نام ")]
        public string NameTest { get; set; }

        [Display(Name = "توضیحات")]
        [AllowHtml]
        [UIHint("RichText")]
        public string Description { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "دانلود اجباری")]
        public bool BoolTest { get; set; }


        //public static explicit operator VersionIphone(TestVm model)
        //{
        //    var obj = new VersionIphone
        //    {
        //        VersionStaticValue = model.VersionStaticValue,
        //        DescriptionAllowHtml = model.DescriptionAllowHtml,
        //        DownloadUrl = model.DownloadUrl,
        //        IsForceDownload = model.IsForceDownload,
        //        VersionApp = model.VersionApp,
        //        CreateDate = model.CreateDate,
        //        VersionName = model.VersionName,
        //        Id = model.Id,
        //    };
        //    return obj;
        //}

        //public static explicit operator TestVm(VersionIphone model)
        //{
        //    var obj = new TestVm
        //    {
        //        VersionStaticValue = model.VersionStaticValue,
        //        DescriptionAllowHtml = model.DescriptionAllowHtml,
        //        DownloadUrl = model.DownloadUrl,
        //        IsForceDownload = model.IsForceDownload,
        //        VersionApp = (int) model.VersionApp,
        //        VersionName = model.VersionName,
        //        CreateDate = model.CreateDate,
        //        Id = model.Id,
        //    };
        //    return obj;
        //}


        //public static IEnumerable<TestVm> ToIEnumerable(IEnumerable<VersionIphone> models)
        //{
        //    var product = models.Select(model => new TestVm
        //    {
        //        VersionStaticValue = model.VersionStaticValue,
        //        DescriptionAllowHtml = model.DescriptionAllowHtml,
        //        DownloadUrl = model.DownloadUrl,
        //        IsForceDownload = model.IsForceDownload,
        //        VersionApp = (int)model.VersionApp,
        //        VersionName = model.VersionName,
        //        CreateDate = model.CreateDate,
        //        Id = model.Id,
        //    });
        //    return product;
        //}
    }
}