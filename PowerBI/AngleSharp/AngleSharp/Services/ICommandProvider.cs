using System;
using AngleSharp.Commands;

namespace AngleSharp.Services
{
	// Token: 0x02000025 RID: 37
	public interface ICommandProvider
	{
		// Token: 0x0600011B RID: 283
		ICommand GetCommand(string name);
	}
}
