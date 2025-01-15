using System;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x02000326 RID: 806
	public interface IEscapePowerQueryM
	{
		// Token: 0x060011C8 RID: 4552
		string EscapeIdentifier(string identifier);

		// Token: 0x060011C9 RID: 4553
		string EscapeFieldIdentifier(string fieldIdentifier);

		// Token: 0x060011CA RID: 4554
		string EscapeString(string s);
	}
}
