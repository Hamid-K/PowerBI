using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000285 RID: 645
	[Serializable]
	internal sealed class FunctionReportFolder : BaseInternalExpression
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0002FEAC File Offset: 0x0002E0AC
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x0002FEB4 File Offset: 0x0002E0B4
		public string Folder
		{
			get
			{
				return this._Folder;
			}
			set
			{
				this._Folder = value;
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0002FEBD File Offset: 0x0002E0BD
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.String;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0002FEC1 File Offset: 0x0002E0C1
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Globals!ReportFolder";
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0002FEC8 File Offset: 0x0002E0C8
		public override object Evaluate()
		{
			return "";
		}

		// Token: 0x040006A9 RID: 1705
		[NonSerialized]
		private string _Folder;
	}
}
