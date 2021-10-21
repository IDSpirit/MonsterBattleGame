using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterBattleUI.Models
{
    public class OutcomeModel
    {
        private bool _isError;

        public bool isError
        {
            get { return _isError; }
            set { _isError = value; }
        }

        private List<string> _messages;

        public List<string> messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        public OutcomeModel(bool isError, List<string> messages)
        {
            this.isError = isError;
            this.messages = messages;
        }

    }
}
