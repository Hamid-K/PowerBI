using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000065 RID: 101
	public interface IStreamablePowerBIProjectPartContent : IFromPBIProjectFile
	{
		// Token: 0x060002C9 RID: 713
		Task<Stream> GetStreamAsync();
	}
}
