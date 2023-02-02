using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IFeedbackManager
    {
        public FeedbackModel AddFeedback(FeedbackModel feedbackModel);
        public List<FeedbackModel> GetAllFeedback(int BookID);
    }
}
