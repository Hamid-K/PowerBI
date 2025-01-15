using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012D7 RID: 4823
	public class DelegatingModule : Module
	{
		// Token: 0x06007F5F RID: 32607 RVA: 0x001B3FEF File Offset: 0x001B21EF
		public DelegatingModule(Module module)
		{
			this.module = module;
		}

		// Token: 0x1700227A RID: 8826
		// (get) Token: 0x06007F60 RID: 32608 RVA: 0x001B3FFE File Offset: 0x001B21FE
		public Module BaseModule
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x1700227B RID: 8827
		// (get) Token: 0x06007F61 RID: 32609 RVA: 0x001B4006 File Offset: 0x001B2206
		public override string Name
		{
			get
			{
				return this.module.Name;
			}
		}

		// Token: 0x1700227C RID: 8828
		// (get) Token: 0x06007F62 RID: 32610 RVA: 0x001B4013 File Offset: 0x001B2213
		public override string Location
		{
			get
			{
				return this.module.Location;
			}
		}

		// Token: 0x1700227D RID: 8829
		// (get) Token: 0x06007F63 RID: 32611 RVA: 0x001B4020 File Offset: 0x001B2220
		public override string Version
		{
			get
			{
				return this.module.Version;
			}
		}

		// Token: 0x1700227E RID: 8830
		// (get) Token: 0x06007F64 RID: 32612 RVA: 0x001B402D File Offset: 0x001B222D
		public override ModuleKind Kind
		{
			get
			{
				return this.module.Kind;
			}
		}

		// Token: 0x1700227F RID: 8831
		// (get) Token: 0x06007F65 RID: 32613 RVA: 0x001B403A File Offset: 0x001B223A
		public override Import[] Imports
		{
			get
			{
				return this.module.Imports;
			}
		}

		// Token: 0x17002280 RID: 8832
		// (get) Token: 0x06007F66 RID: 32614 RVA: 0x001B4047 File Offset: 0x001B2247
		public override Keys ExportKeys
		{
			get
			{
				return this.module.ExportKeys;
			}
		}

		// Token: 0x17002281 RID: 8833
		// (get) Token: 0x06007F67 RID: 32615 RVA: 0x001B4054 File Offset: 0x001B2254
		public override Keys SectionKeys
		{
			get
			{
				return this.module.SectionKeys;
			}
		}

		// Token: 0x17002282 RID: 8834
		// (get) Token: 0x06007F68 RID: 32616 RVA: 0x001B4061 File Offset: 0x001B2261
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return this.module.DataSources;
			}
		}

		// Token: 0x17002283 RID: 8835
		// (get) Token: 0x06007F69 RID: 32617 RVA: 0x001B406E File Offset: 0x001B226E
		public override RecordValue Metadata
		{
			get
			{
				return this.module.Metadata;
			}
		}

		// Token: 0x06007F6A RID: 32618 RVA: 0x001B407B File Offset: 0x001B227B
		public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
		{
			return this.module.Link(environment, hostEnvironment);
		}

		// Token: 0x06007F6B RID: 32619 RVA: 0x001B408A File Offset: 0x001B228A
		public override SourceLocation GetExportLocation(string name)
		{
			return this.module.GetExportLocation(name);
		}

		// Token: 0x06007F6C RID: 32620 RVA: 0x001B4098 File Offset: 0x001B2298
		public override SourceLocation[] GetImportLocations(int import)
		{
			return this.module.GetImportLocations(import);
		}

		// Token: 0x040045A9 RID: 17833
		protected const string Shared = "Shared";

		// Token: 0x040045AA RID: 17834
		protected const string Section = "Section";

		// Token: 0x040045AB RID: 17835
		protected static readonly Keys SharedKeys = Keys.New("Shared");

		// Token: 0x040045AC RID: 17836
		protected static readonly Keys Section_Keys = Keys.New("Section");

		// Token: 0x040045AD RID: 17837
		private readonly Module module;
	}
}
