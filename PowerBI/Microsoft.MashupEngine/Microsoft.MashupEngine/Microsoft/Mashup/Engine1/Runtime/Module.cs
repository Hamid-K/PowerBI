using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001582 RID: 5506
	public abstract class Module : IModule
	{
		// Token: 0x17002415 RID: 9237
		// (get) Token: 0x06008924 RID: 35108 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual string Name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002416 RID: 9238
		// (get) Token: 0x06008925 RID: 35109 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual string Location
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002417 RID: 9239
		// (get) Token: 0x06008926 RID: 35110 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual string Version
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002418 RID: 9240
		// (get) Token: 0x06008927 RID: 35111 RVA: 0x00002105 File Offset: 0x00000305
		public virtual ModuleKind Kind
		{
			get
			{
				return ModuleKind.Module;
			}
		}

		// Token: 0x17002419 RID: 9241
		// (get) Token: 0x06008928 RID: 35112 RVA: 0x001D088A File Offset: 0x001CEA8A
		public virtual Import[] Imports
		{
			get
			{
				return EmptyArray<Import>.Instance;
			}
		}

		// Token: 0x1700241A RID: 9242
		// (get) Token: 0x06008929 RID: 35113
		public abstract Keys ExportKeys { get; }

		// Token: 0x1700241B RID: 9243
		// (get) Token: 0x0600892A RID: 35114 RVA: 0x00019E61 File Offset: 0x00018061
		public virtual RecordValue Metadata
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x1700241C RID: 9244
		// (get) Token: 0x0600892B RID: 35115 RVA: 0x001D0891 File Offset: 0x001CEA91
		public virtual ResourceKindInfo[] DataSources
		{
			get
			{
				return EmptyArray<ResourceKindInfo>.Instance;
			}
		}

		// Token: 0x1700241D RID: 9245
		// (get) Token: 0x0600892C RID: 35116 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual ResourceKindInfo DynamicModuleDataSource
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700241E RID: 9246
		// (get) Token: 0x0600892D RID: 35117 RVA: 0x001D0898 File Offset: 0x001CEA98
		IKeys IModule.Exports
		{
			get
			{
				return this.ExportKeys;
			}
		}

		// Token: 0x1700241F RID: 9247
		// (get) Token: 0x0600892E RID: 35118 RVA: 0x001D08A0 File Offset: 0x001CEAA0
		IRecordValue IModule.Metadata
		{
			get
			{
				return this.Metadata;
			}
		}

		// Token: 0x17002420 RID: 9248
		// (get) Token: 0x0600892F RID: 35119 RVA: 0x0003389B File Offset: 0x00031A9B
		public virtual Keys SectionKeys
		{
			get
			{
				return Keys.Empty;
			}
		}

		// Token: 0x06008930 RID: 35120 RVA: 0x001D08A8 File Offset: 0x001CEAA8
		public virtual RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(Linker.LinkedKeys, new Value[]
			{
				Value.Null,
				this.GetSharedExports(environment, hostEnvironment),
				this.GetSectionExports(environment, hostEnvironment)
			});
		}

		// Token: 0x06008931 RID: 35121 RVA: 0x00019E61 File Offset: 0x00018061
		protected virtual RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.Empty;
		}

		// Token: 0x06008932 RID: 35122 RVA: 0x00019E61 File Offset: 0x00018061
		protected virtual RecordValue GetSectionExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.Empty;
		}

		// Token: 0x06008933 RID: 35123 RVA: 0x001D08D8 File Offset: 0x001CEAD8
		public virtual SourceLocation GetExportLocation(string name)
		{
			return SourceLocation.None;
		}

		// Token: 0x06008934 RID: 35124 RVA: 0x001D08DF File Offset: 0x001CEADF
		public virtual SourceLocation[] GetImportLocations(int import)
		{
			return new SourceLocation[] { SourceLocation.None };
		}
	}
}
