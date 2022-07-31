using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClient.Data.FileSending
{
    public class FileBlock
    {
        public Guid StreamId { get; set; }
        public byte[] Data { get; set; }
        public int Index { get; set; }
        public bool EndOfStream { get; set; }
        public Guid AppId { get; set; }
		public Action<FileBlock> Completed { get; set; }

		public void Execute(object sender, object message)
		{
			Completed?.Invoke(this);
		}
	}
}
