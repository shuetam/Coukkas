using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Coukkas.Infrastructure;
using Coukkas.Infrastructure.Services;
using Coukkas.Infrastructure.Repositories.DTOS;
using Microsoft.AspNetCore.Authorization;
using Coukkas.Infrastructure.FromBodyCommands;
using Coukkas.Core.Domain;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions;
using Microsoft.Net.Http.Headers;


namespace Coukkas.Api.Controllers
{
    
    public class FenceController : ValuesController 
    {
        private readonly  IUserService _userService;
        private readonly  IFenceService _fenceService;

        private readonly IHostingEnvironment _hostingEnvironment;


        public FenceController (IUserService userService, IFenceService fenceService, IHostingEnvironment hostingEnvironment )
        {
            _userService = userService;
            _fenceService = fenceService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task <IActionResult> CreateFence([FromBody] FenceCreated fence)
        {
            Guid fenceID = Guid.NewGuid();
         await _fenceService.CreateAsync(fenceID, UserId, fence.Name, fence.Description, fence.Category, DateTime.UtcNow, DateTime.UtcNow.AddDays(fence.Days), fence.lat, fence.lon, fence.Radius);
           return Created($"fences/{fenceID}", null);
        } 


        [HttpPost("createmany")]
        [Authorize]
        public async Task <IActionResult> CreateFence([FromBody] List <FenceCreated> fences)
        {
            foreach(var fence in fences)
            {
            Guid fenceID = Guid.NewGuid();
            await _fenceService.CreateAsync(fenceID, UserId, fence.Name, fence.Description, fence.Category, DateTime.UtcNow, DateTime.UtcNow.AddDays(fence.Days), fence.lat, fence.lon, fence.Radius);
            }
            return Created($"fences/many", null);
        } 

        
     
        [HttpGet("outfences")]
        [Authorize]
        public async Task <IActionResult> GetOutFances()
        {
            var fences = await _fenceService.GetNotAvailableAsync(UserId);
            return Json(fences);
        }


        [HttpGet("infences")]
        [Authorize]
        public async Task <IActionResult> GetInFances()
        {
            var fences = await _fenceService.GetAvailableAsync(UserId);
            return Json(fences);
        }


        [HttpGet("myfences")]
        [Authorize]   
        public async Task <IActionResult> GetFences()
        {   
            var fences = await _fenceService.GetByOwnerAsync(UserId);
           
             return Json(fences);    
        }


        
        [HttpGet("alldatafences")]
        public async Task <IActionResult> GetAllFences()
        {   
            var fences = await _fenceService.GetAllAsync();
           
             return Json(fences);    
        }


        [HttpPost("addcoupons")]
        [Authorize]  
        public async Task <IActionResult> AddCoupons([FromBody] CouponCreated command) 
        {
            await _fenceService.AddCoupons(command.FenceId, command.amount);
            return Created("/coupon", null);
        }

        [HttpDelete("delete_fence/{fenceID}")]
        [Authorize]
        public async Task <IActionResult> DeleteFence([FromRoute] Guid fenceID)
        {
            var fences = await _fenceService.GetByOwnerAsync(UserId);
            
            await _fenceService.DeleteAsync(fences.Single(f => f.Id==fenceID).Id);
            return NoContent();
        }



        [HttpGet("GetImage{o}")]
    public async Task<IActionResult> GetImage(string o)
    {
        var image =  System.IO.File.OpenRead($@"C:\temp\prze{o}.jpg");
      
        return File(image, "image/jpeg");
    }

     [HttpGet("FromData{l}")] 
        public async Task<FileStreamResult> ViewImage(int l) 
        { 
          var memory =  await _fenceService.GetImage(l);
          return new FileStreamResult(memory, "image/jpeg");
        } 

    [HttpGet("test")] 
        public async Task <IActionResult> test () 
        { 
        var paths = new List<string>();
        paths.Add(_hostingEnvironment.WebRootPath);
        paths.Add(_hostingEnvironment.ContentRootPath);
        paths.Add(Path.Combine(_hostingEnvironment.ContentRootPath, "images"));
        return Json(paths);
        } 

  [HttpPost("uploadimage")] 
        public async Task <IActionResult> UploadImage(IList<IFormFile> files) 
        {
          if (files == null) return BadRequest("File Missing");
          long size = files.Sum(f => f.Length);
          string path = _hostingEnvironment.ContentRootPath + "/" + files[0].FileName;

          //  string path = "C:\\Users\\Mateusz\\Dropbox\\App\\Coukkas\\src\\Coukkas.Api\\images1";

   
     using (var stream = System.IO.File.OpenWrite(path))
     {
       files[0].CopyTo(stream);
     }
     
       return Ok(new { path});
   }


 [HttpPost("uploadimage1")]
    public async Task<IActionResult> Upload(IList<IFormFile> files) 
    {
        try
        {
        var uploads = _hostingEnvironment.ContentRootPath + "\\images1";
        foreach (var file in files) {
            if (file.Length > 0) {
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }
            }
        }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Ok();
    }




    [HttpPost("UploadFiles")]
public async Task<IActionResult> Post(List<IFormFile> files)
{
    Console.WriteLine(files.FirstOrDefault().FileName);
    long size = files.Sum(f => f.Length);

    // full path to file in temp location
    var filePath = Path.GetTempFileName();

    foreach (var formFile in files)
    {
        if (formFile.Length > 0)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
        }
    }

    // process uploaded files
    // Don't rely on or trust the FileName property without validation.

    return Ok(new { count = files.Count, size, filePath});
}

}
}







        //var filename = file.FileName;
            
          //  var stream =  file.OpenReadStream()

   /*  file.Save
        string path = _hostingEnvironment.ContentRootPath + "\\images";
            stream.CopyToAsync(path)
        
        using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            } */

/* 
        return Json(filename);
        
        } 

    }
} */