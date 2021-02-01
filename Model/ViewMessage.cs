using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ViewMessage
    {
        public string Message { get; set; } = string.Empty;

        public string StatusID { get; set; } = string.Empty;
        public bool isEnabledAnalyzeChecked { get; set; }

        public List<Result> ExternalLinkResults = new List<Result>();

        public List<Result> MetaWordResults = new List<Result>();

        public List<Result> WordResults = new List<Result>();
    }
}
