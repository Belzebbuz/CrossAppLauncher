using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClient.Data.FileSending
{
	public class FileReader : IDisposable
	{
		public FileReader(string file, Guid appId, Guid streamId)
		{
			mInfo = new FileInfo(file);
			AppId = appId;
			StreamId = streamId;
			mPages = (int)(mInfo.Length / mSize);
			if (mInfo.Length % mSize > 0)
				mPages++;
			FileSize = mInfo.Length;
			mBuffer = new byte[mSize];
			mReader = mInfo.OpenRead();
		}

		private Guid AppId;
		private Guid StreamId;

		private Stream mReader;

		private byte[] mBuffer;

		private FileInfo mInfo;

		private int mPages;

		private int mSize = 1024 * 16;

		private int mIndex;

		public int Index => mIndex;

		public int Size => mSize;

		public int Pages => mPages;

		public long FileSize { get; private set; }

		public long CompletedSize { get; private set; }

		public bool Completed => mIndex == mPages;

		public FileBlock Next()
		{
			FileBlock result = new FileBlock();
			result.AppId = AppId;
			result.StreamId = StreamId;
			byte[] data;
			if (mIndex == mPages - 1)
			{
				data = new byte[mInfo.Length - mIndex * mSize];
				result.EndOfStream = true;
			}
			else
			{
				data = mBuffer;
			}
			CompletedSize += data.Length;
			mReader.Read(data, 0, data.Length);
			result.Index = mIndex;
			result.Data = data;
			mIndex++;


			return result;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}

}
