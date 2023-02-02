using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStoreApplication.Controllers
{
    [Route("BookStore/[controller]")]
    [Authorize(Roles = Role.User)]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager feedbackManager;
        public FeedbackController(IFeedbackManager feedbackManager)
        {
            this.feedbackManager = feedbackManager;
        }
        [HttpPost]
        [Route("BookStore/AddFeedback")]
        public IActionResult AddFeedback(FeedbackModel feedbackModel)
        {
            try
            {
                feedbackModel.UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                FeedbackModel feedbackData = this.feedbackManager.AddFeedback(feedbackModel);
                if (feedbackData != null)
                {
                    return this.Ok(new { success = true, message = "Feedback Added Successfully", result = feedbackData });
                }
                return this.Ok(new { success = true, message = "Feedback Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("BookStore/GetAllFeedback")]
        public IActionResult GetAllFeedback(int BookID)
        {
            try
            {
                List<FeedbackModel> feedbackData = this.feedbackManager.GetAllFeedback(BookID);
                if (feedbackData != null)
                {
                    return this.Ok(new { success = true, message = "Feedback Get Successfully", result = feedbackData });
                }
                return this.Ok(new { success = true, message = "Feedback is Empty" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
