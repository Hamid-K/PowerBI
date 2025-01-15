using System;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x0200088D RID: 2189
	public class EXCSATRD : EXCSAT
	{
		// Token: 0x0600458C RID: 17804 RVA: 0x000F00A4 File Offset: 0x000EE2A4
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.EXCSATRD);
			if (!base.IsPing)
			{
				if (base.Extnam != null)
				{
					writer.WriteScalar(CodePoint.EXTNAM, base.Extnam);
				}
				if (base.MgrLvlls != null && base.MgrLvlls.Count > 0)
				{
					this.WriteMgrlvlls(writer);
				}
				if (base.Srvclsnm != null)
				{
					writer.WriteScalar(CodePoint.SRVCLSNM, base.Srvclsnm);
				}
				if (base.Srvnam != null)
				{
					writer.WriteScalar(CodePoint.SRVNAM, base.Srvnam);
				}
				if (base.Srvrlslv != null)
				{
					writer.WriteScalar(CodePoint.SRVRLSLV, base.Srvrlslv);
				}
			}
			writer.WriteEndDdm();
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x000F0150 File Offset: 0x000EE350
		private void WriteMgrlvlls(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.MGRLVLLS);
			foreach (ManagerCodePoint managerCodePoint in base.MgrLvlls.Keys)
			{
				writer.WriteCodePointAnd2Bytes((CodePoint)managerCodePoint, base.MgrLvlls[managerCodePoint], EndianType.BigEndian);
			}
			writer.WriteEndDdm();
		}
	}
}
