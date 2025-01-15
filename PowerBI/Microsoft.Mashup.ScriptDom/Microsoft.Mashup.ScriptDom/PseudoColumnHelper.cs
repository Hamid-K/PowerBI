using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000A4 RID: 164
	internal class PseudoColumnHelper : OptionsHelper<ColumnType>
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000BB01 File Offset: 0x00009D01
		private PseudoColumnHelper()
		{
			base.AddOptionMapping(ColumnType.PseudoColumnIdentity, "$IDENTITY", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(ColumnType.PseudoColumnRowGuid, "$ROWGUID", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(ColumnType.PseudoColumnAction, "$ACTION", SqlVersionFlags.TSql100AndAbove);
			base.AddOptionMapping(ColumnType.PseudoColumnCuid, "$CUID", SqlVersionFlags.TSql100AndAbove);
		}

		// Token: 0x040003D7 RID: 983
		internal static readonly PseudoColumnHelper Instance = new PseudoColumnHelper();
	}
}
