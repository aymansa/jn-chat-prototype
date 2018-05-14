using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chatproto
{
    
    public class ChatViewModel
    {
        public List<Interaction> Interactions { get; set; }
        public ChatViewModel()
        {
            Interactions = GetPyxusChatHistory(1);
        }
        #region GetChatHistory
        public static List<Interaction> GetPyxusChatHistory(int profileID)
        {
            List<Interaction> _temp = new List<Interaction>();
            _temp.Add(new Interaction()
            {
                encounterID = 1,
                IsUser = false,
                SpokenWord = "Hello, what is your name?",
                showMessage = true
            });
            _temp.Add(new Interaction()
            {
                encounterID = 2,
                IsUser = true,
                SpokenWord = "Jon",
                showMessage = false
            });
            _temp.Add(new Interaction()
            {
                encounterID = 3,
                IsUser = false,
                SpokenWord = "Hey Jon, it's really nice to meet you!",
                showMessage = false
            });
            _temp.Add(new Interaction()
            {
                encounterID = 4,
                IsUser = false,
                SpokenWord = "What's your favorite color?",
                showMessage = true
            });
            return _temp.OrderBy(x => x.encounterID).ToList();
        }
        #endregion
    }
}
