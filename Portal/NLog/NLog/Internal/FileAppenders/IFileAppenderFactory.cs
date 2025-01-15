using System;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000162 RID: 354
	internal interface IFileAppenderFactory
	{
		// Token: 0x060010B9 RID: 4281
		BaseFileAppender Open(string fileName, ICreateFileParameters parameters);
	}
}
