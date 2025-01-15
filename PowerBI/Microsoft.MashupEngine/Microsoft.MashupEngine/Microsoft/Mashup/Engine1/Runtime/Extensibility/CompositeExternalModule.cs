using System;
using System.Linq;
using System.Resources;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x020016ED RID: 5869
	public abstract class CompositeExternalModule : ExternalModule
	{
		// Token: 0x0600952E RID: 38190 RVA: 0x001ED7E8 File Offset: 0x001EB9E8
		public CompositeExternalModule(ResourceManager documentationResources, params Module[] modules)
		{
			this.documentationResources = documentationResources;
			this.modules = modules;
			this.linkedModule = Linker.Link(modules, delegate(IError entry)
			{
				throw new InvalidOperationException();
			}, LinkOptions.None);
		}

		// Token: 0x1700270A RID: 9994
		// (get) Token: 0x0600952F RID: 38191 RVA: 0x001ED835 File Offset: 0x001EBA35
		public override ResourceManager DocumentationResources
		{
			get
			{
				return this.documentationResources;
			}
		}

		// Token: 0x1700270B RID: 9995
		// (get) Token: 0x06009530 RID: 38192 RVA: 0x001ED83D File Offset: 0x001EBA3D
		public override Keys ExportKeys
		{
			get
			{
				return this.linkedModule.ExportKeys;
			}
		}

		// Token: 0x1700270C RID: 9996
		// (get) Token: 0x06009531 RID: 38193 RVA: 0x001ED84A File Offset: 0x001EBA4A
		public override Import[] Imports
		{
			get
			{
				return this.linkedModule.Imports;
			}
		}

		// Token: 0x1700270D RID: 9997
		// (get) Token: 0x06009532 RID: 38194 RVA: 0x001ED857 File Offset: 0x001EBA57
		public override ModuleKind Kind
		{
			get
			{
				return this.linkedModule.Kind;
			}
		}

		// Token: 0x1700270E RID: 9998
		// (get) Token: 0x06009533 RID: 38195 RVA: 0x001ED864 File Offset: 0x001EBA64
		public override Keys SectionKeys
		{
			get
			{
				return this.linkedModule.SectionKeys;
			}
		}

		// Token: 0x1700270F RID: 9999
		// (get) Token: 0x06009534 RID: 38196 RVA: 0x001ED871 File Offset: 0x001EBA71
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return this.modules.SelectMany((Module m) => m.DataSources).ToArray<ResourceKindInfo>();
			}
		}

		// Token: 0x06009535 RID: 38197 RVA: 0x001ED8A2 File Offset: 0x001EBAA2
		public override SourceLocation GetExportLocation(string name)
		{
			return this.linkedModule.GetExportLocation(name);
		}

		// Token: 0x06009536 RID: 38198 RVA: 0x001ED8B0 File Offset: 0x001EBAB0
		public override SourceLocation[] GetImportLocations(int import)
		{
			return this.linkedModule.GetImportLocations(import);
		}

		// Token: 0x06009537 RID: 38199 RVA: 0x001ED8BE File Offset: 0x001EBABE
		public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
		{
			return this.linkedModule.Link(environment, hostEnvironment);
		}

		// Token: 0x04004F4D RID: 20301
		private readonly ResourceManager documentationResources;

		// Token: 0x04004F4E RID: 20302
		private readonly Module linkedModule;

		// Token: 0x04004F4F RID: 20303
		private readonly Module[] modules;
	}
}
