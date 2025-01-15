using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x020017A4 RID: 6052
	public class RuntimeModule : Module
	{
		// Token: 0x06009902 RID: 39170 RVA: 0x001F9A04 File Offset: 0x001F7C04
		public RuntimeModule(Import[] imports, SourceLocation[][] importInfo, Assembly assembly)
			: this(null, imports, importInfo, Keys.Empty, Keys.Empty, EmptyArray<SourceLocation>.Instance, assembly, ModuleKind.Expression)
		{
			this.metadata = RecordValue.Empty;
		}

		// Token: 0x06009903 RID: 39171 RVA: 0x001F9A38 File Offset: 0x001F7C38
		public RuntimeModule(string name, RecordValue metadata, Import[] imports, SourceLocation[][] importInfo, Keys exports, Keys sectionKeys, SourceLocation[] exportInfo, Assembly assembly)
			: this(name, imports, importInfo, exports, sectionKeys, exportInfo, assembly, ModuleKind.Module)
		{
			this.metadata = metadata;
		}

		// Token: 0x06009904 RID: 39172 RVA: 0x001F9A60 File Offset: 0x001F7C60
		private RuntimeModule(string name, Import[] imports, SourceLocation[][] importInfo, Keys exports, Keys sectionKeys, SourceLocation[] exportInfo, Assembly assembly, ModuleKind kind)
		{
			this.name = name;
			this.imports = imports;
			this.importInfo = importInfo;
			this.exports = exports;
			this.sectionKeys = sectionKeys;
			this.exportInfo = exportInfo;
			this.assembly = assembly;
			this.kind = kind;
		}

		// Token: 0x170027A3 RID: 10147
		// (get) Token: 0x06009905 RID: 39173 RVA: 0x001F9AB0 File Offset: 0x001F7CB0
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170027A4 RID: 10148
		// (get) Token: 0x06009906 RID: 39174 RVA: 0x001F9AB8 File Offset: 0x001F7CB8
		public override Import[] Imports
		{
			get
			{
				return this.imports;
			}
		}

		// Token: 0x170027A5 RID: 10149
		// (get) Token: 0x06009907 RID: 39175 RVA: 0x001F9AC0 File Offset: 0x001F7CC0
		public override RecordValue Metadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x06009908 RID: 39176 RVA: 0x001F9AC8 File Offset: 0x001F7CC8
		public override SourceLocation[] GetImportLocations(int import)
		{
			return this.importInfo[import];
		}

		// Token: 0x06009909 RID: 39177 RVA: 0x001F9AD4 File Offset: 0x001F7CD4
		public override SourceLocation GetExportLocation(string name)
		{
			int num;
			if (this.ExportKeys.TryGetKeyIndex(name, out num))
			{
				return this.exportInfo[num];
			}
			return SourceLocation.None;
		}

		// Token: 0x170027A6 RID: 10150
		// (get) Token: 0x0600990A RID: 39178 RVA: 0x001F9AFF File Offset: 0x001F7CFF
		public override Keys ExportKeys
		{
			get
			{
				return this.exports;
			}
		}

		// Token: 0x170027A7 RID: 10151
		// (get) Token: 0x0600990B RID: 39179 RVA: 0x001F9B07 File Offset: 0x001F7D07
		public override Keys SectionKeys
		{
			get
			{
				return this.sectionKeys;
			}
		}

		// Token: 0x170027A8 RID: 10152
		// (get) Token: 0x0600990C RID: 39180 RVA: 0x001F9B0F File Offset: 0x001F7D0F
		public override ModuleKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x170027A9 RID: 10153
		// (get) Token: 0x0600990D RID: 39181 RVA: 0x001F9B18 File Offset: 0x001F7D18
		public override string Version
		{
			get
			{
				Value value;
				if (this.Metadata.TryGetValue("Version", out value) && value.IsText)
				{
					return value.AsString;
				}
				return null;
			}
		}

		// Token: 0x0600990E RID: 39182 RVA: 0x001F9B49 File Offset: 0x001F7D49
		private Value Bind(Value environment)
		{
			return this.GetLinker().Invoke(environment);
		}

		// Token: 0x0600990F RID: 39183 RVA: 0x001F9B58 File Offset: 0x001F7D58
		public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
		{
			RecordValue recordValue = this.Bind(environment).AsRecord;
			Value value;
			if (this.name != null && recordValue.TryGetValue("Shared", out value) && value.IsRecord)
			{
				recordValue = RecordValue.Combine(ListValue.New(new Value[]
				{
					recordValue,
					RecordValue.New(RuntimeModule.SharedKeys, new Value[] { this.FixIdentities(hostEnvironment, value.AsRecord) })
				}));
			}
			return recordValue;
		}

		// Token: 0x06009910 RID: 39184 RVA: 0x001F9BCA File Offset: 0x001F7DCA
		private FunctionValue GetLinker()
		{
			return this.assembly.Function.Invoke().AsFunction;
		}

		// Token: 0x06009911 RID: 39185 RVA: 0x001F9BE4 File Offset: 0x001F7DE4
		private RecordValue FixIdentities(IEngineHost engineHost, RecordValue exports)
		{
			RuntimeModule.<>c__DisplayClass33_0 CS$<>8__locals1 = new RuntimeModule.<>c__DisplayClass33_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.exports = exports;
			RuntimeModule.<>c__DisplayClass33_0 CS$<>8__locals2 = CS$<>8__locals1;
			ICultureService cultureService = engineHost.QueryService<ICultureService>();
			string text;
			if (cultureService == null)
			{
				text = null;
			}
			else
			{
				ICulture defaultCulture = cultureService.DefaultCulture;
				text = ((defaultCulture != null) ? defaultCulture.Name : null);
			}
			CS$<>8__locals2.defaultCulture = text;
			return RecordValue.New(CS$<>8__locals1.exports.Keys, (int i) => CS$<>8__locals1.<>4__this.FixIdentities(CS$<>8__locals1.exports.Keys[i], CS$<>8__locals1.defaultCulture, CS$<>8__locals1.exports[i]));
		}

		// Token: 0x06009912 RID: 39186 RVA: 0x001F9C48 File Offset: 0x001F7E48
		private Value FixIdentities(string name, string culture, Value value)
		{
			if (value.IsFunction)
			{
				FunctionValue asFunction = value.AsFunction;
				if (asFunction.FunctionIdentity is MembersFunctionValue)
				{
					return new RuntimeModule.FunctionIdentityFunctionValue(this, name, culture, asFunction);
				}
			}
			return value;
		}

		// Token: 0x0400511F RID: 20767
		private const string Shared = "Shared";

		// Token: 0x04005120 RID: 20768
		private static readonly Keys SharedKeys = Keys.New("Shared");

		// Token: 0x04005121 RID: 20769
		private readonly string name;

		// Token: 0x04005122 RID: 20770
		private readonly RecordValue metadata;

		// Token: 0x04005123 RID: 20771
		private readonly Import[] imports;

		// Token: 0x04005124 RID: 20772
		private readonly Keys exports;

		// Token: 0x04005125 RID: 20773
		private readonly Keys sectionKeys;

		// Token: 0x04005126 RID: 20774
		private readonly Assembly assembly;

		// Token: 0x04005127 RID: 20775
		private readonly ModuleKind kind;

		// Token: 0x04005128 RID: 20776
		private readonly SourceLocation[][] importInfo;

		// Token: 0x04005129 RID: 20777
		private readonly SourceLocation[] exportInfo;

		// Token: 0x020017A5 RID: 6053
		private sealed class FunctionIdentityFunctionValue : DelegatingFunctionValue, IFunctionIdentity, IEquatable<IFunctionIdentity>
		{
			// Token: 0x06009914 RID: 39188 RVA: 0x001F9C8D File Offset: 0x001F7E8D
			public FunctionIdentityFunctionValue(RuntimeModule module, string name, string culture, FunctionValue function)
				: base(function)
			{
				this.module = module;
				this.name = name;
				this.culture = culture;
			}

			// Token: 0x170027AA RID: 10154
			// (get) Token: 0x06009915 RID: 39189 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06009916 RID: 39190 RVA: 0x001F9CAC File Offset: 0x001F7EAC
			protected override FunctionValue Wrap(FunctionValue function)
			{
				return new RuntimeModule.FunctionIdentityFunctionValue(this.module, this.name, this.culture, function);
			}

			// Token: 0x06009917 RID: 39191 RVA: 0x001F9CC6 File Offset: 0x001F7EC6
			public override int GetHashCode()
			{
				return this.name.GetHashCode();
			}

			// Token: 0x06009918 RID: 39192 RVA: 0x001F9CD4 File Offset: 0x001F7ED4
			public bool Equals(IFunctionIdentity other)
			{
				RuntimeModule.FunctionIdentityFunctionValue functionIdentityFunctionValue = other as RuntimeModule.FunctionIdentityFunctionValue;
				if (functionIdentityFunctionValue != null)
				{
					return this.name == functionIdentityFunctionValue.name && this.module.Name == functionIdentityFunctionValue.module.Name && this.module.Version == functionIdentityFunctionValue.module.Version && this.culture == functionIdentityFunctionValue.culture;
				}
				return base.Function.FunctionIdentity.Equals(other);
			}

			// Token: 0x0400512A RID: 20778
			private readonly RuntimeModule module;

			// Token: 0x0400512B RID: 20779
			private readonly string culture;

			// Token: 0x0400512C RID: 20780
			private readonly string name;
		}
	}
}
