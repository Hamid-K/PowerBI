using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F01 RID: 3841
	internal class AnalysisServicesCatalog
	{
		// Token: 0x060065CA RID: 26058 RVA: 0x0015E9E2 File Offset: 0x0015CBE2
		public AnalysisServicesCatalog(AnalysisServicesService service, string name)
		{
			this.service = service;
			this.name = name;
		}

		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x060065CB RID: 26059 RVA: 0x0015E9F8 File Offset: 0x0015CBF8
		public AnalysisServicesService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x060065CC RID: 26060 RVA: 0x0015EA00 File Offset: 0x0015CC00
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x060065CD RID: 26061 RVA: 0x0015EA08 File Offset: 0x0015CC08
		public IList<AnalysisServicesMdxCube> Cubes
		{
			get
			{
				if (this.cubes == null)
				{
					this.cubes = this.GetCubes();
				}
				return this.cubes;
			}
		}

		// Token: 0x060065CE RID: 26062 RVA: 0x0015EA24 File Offset: 0x0015CC24
		private IList<AnalysisServicesMdxCube> GetCubes()
		{
			IList<AnalysisServicesMdxCube> list2;
			using (IDataReader dataReader = this.service.ExecuteCommand(this.service.MetadataCache, CommandBehavior.Default, "select [CUBE_NAME], [BASE_CUBE_NAME], [CUBE_CAPTION] from $system.mdschema_cubes where [CUBE_SOURCE] = 1", Array.Empty<KeyValuePair<string, object>>()))
			{
				List<AnalysisServicesMdxCube> list = new List<AnalysisServicesMdxCube>();
				while (dataReader.Read())
				{
					list.Add(new AnalysisServicesMdxCube(this.service, (string)dataReader["CUBE_NAME"], dataReader["BASE_CUBE_NAME"] as string, dataReader["CUBE_CAPTION"] as string));
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x040037F0 RID: 14320
		private readonly AnalysisServicesService service;

		// Token: 0x040037F1 RID: 14321
		private readonly string name;

		// Token: 0x040037F2 RID: 14322
		private IList<AnalysisServicesMdxCube> cubes;
	}
}
