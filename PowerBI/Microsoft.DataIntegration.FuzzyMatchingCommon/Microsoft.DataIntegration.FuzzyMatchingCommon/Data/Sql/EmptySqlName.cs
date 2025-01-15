using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	internal class EmptySqlName : SqlName
	{
		// Token: 0x0600033A RID: 826 RVA: 0x00016745 File Offset: 0x00014945
		public override bool TryParse(string strIdentifier)
		{
			if (strIdentifier != null && strIdentifier.Length > 0)
			{
				throw new InvalidOperationException("Can not call TryParse on EmptySqlName object.");
			}
			return base.TryParse(strIdentifier);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00016765 File Offset: 0x00014965
		public override void SetPart(SqlName.Part p, string str)
		{
			if (str != null && str.Length > 0)
			{
				throw new InvalidOperationException("Can not call SetPart on EmptySqlName object.");
			}
			base.SetPart(p, str);
		}
	}
}
