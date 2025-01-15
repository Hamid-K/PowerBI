using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200179C RID: 6044
	public static class Linker
	{
		// Token: 0x060098DB RID: 39131 RVA: 0x001F8E00 File Offset: 0x001F7000
		public static Module Link(IList<Module> modules, Action<IError> log, LinkOptions options = LinkOptions.None)
		{
			if ((options & LinkOptions.ExportFirstModule) != LinkOptions.None && modules.Count == 0)
			{
				throw new InvalidOperationException();
			}
			int num = 0;
			int num2 = 0;
			foreach (Module module in modules)
			{
				num += module.ExportKeys.Length;
				if (module.Name != null)
				{
					num2++;
				}
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>(num);
			Dictionary<string, int> dictionary2 = null;
			Module module2 = null;
			int num3 = 0;
			foreach (Module module3 in modules)
			{
				if (module3.Kind == ModuleKind.Expression)
				{
					if (module2 != null || (options & LinkOptions.ExportFirstModule) != LinkOptions.None)
					{
						throw new InvalidOperationException();
					}
					module2 = module3;
				}
				if (module3.Name != null)
				{
					if (dictionary2 == null)
					{
						dictionary2 = new Dictionary<string, int>(num2);
					}
					if (!dictionary2.ContainsKey(module3.Name))
					{
						dictionary2.Add(module3.Name, num3);
					}
					else
					{
						log(SourceErrors.DuplicateSection(SourceLocation.None, module3.Name));
					}
				}
				foreach (string text in module3.ExportKeys)
				{
					int num4;
					if (!dictionary.TryGetValue(text, out num4))
					{
						dictionary.Add(text, num3);
					}
					else
					{
						if (num4 != -1)
						{
							SourceLocation exportLocation = modules[num4].GetExportLocation(text);
							log(SourceErrors.DuplicateExport(exportLocation, text));
						}
						SourceLocation exportLocation2 = module3.GetExportLocation(text);
						log(SourceErrors.DuplicateExport(exportLocation2, text));
						dictionary[text] = -1;
					}
				}
				num3++;
			}
			HashSet<Import> hashSet = null;
			foreach (Module module4 in modules)
			{
				for (int i = 0; i < module4.Imports.Length; i++)
				{
					Import import = module4.Imports[i];
					if (import.Section == null)
					{
						if (!dictionary.ContainsKey(import.Name))
						{
							if ((options & LinkOptions.ExplicitEnvironment) == LinkOptions.None)
							{
								Linker.AddUnboundImport(ref hashSet, import);
							}
							else
							{
								foreach (SourceLocation sourceLocation in module4.GetImportLocations(i))
								{
									log(SourceErrors.UnknownIdentifier(sourceLocation, import.Section, import.Name));
								}
							}
						}
					}
					else if (dictionary2 != null && dictionary2.TryGetValue(import.Section, out num3))
					{
						if (!modules[num3].SectionKeys.Contains(import.Name))
						{
							if ((options & LinkOptions.IgnoreUnresolvedImports) != LinkOptions.None)
							{
								Linker.AddUnboundImport(ref hashSet, import);
							}
							else
							{
								foreach (SourceLocation sourceLocation2 in module4.GetImportLocations(i))
								{
									log(SourceErrors.UnknownIdentifier(sourceLocation2, import.Section, import.Name));
								}
							}
						}
					}
					else if ((options & LinkOptions.IgnoreUnresolvedImports) != LinkOptions.None)
					{
						Linker.AddUnboundImport(ref hashSet, import);
					}
					else
					{
						foreach (SourceLocation sourceLocation3 in module4.GetImportLocations(i))
						{
							log(SourceErrors.UnknownSection(sourceLocation3, import.Section));
						}
					}
				}
			}
			Import[] array2;
			if (hashSet != null)
			{
				array2 = hashSet.ToArray<Import>();
			}
			else
			{
				array2 = EmptyArray<Import>.Instance;
			}
			Module module5 = (((options & LinkOptions.ExportFirstModule) != LinkOptions.None) ? modules[0] : null);
			bool flag = (options & LinkOptions.ExplicitEnvironment) > LinkOptions.None;
			return new Linker.LinkedModule(array2, dictionary, dictionary2, modules, module5, flag);
		}

		// Token: 0x060098DC RID: 39132 RVA: 0x001F91F0 File Offset: 0x001F73F0
		private static void AddUnboundImport(ref HashSet<Import> unboundImports, Import import)
		{
			if (unboundImports == null)
			{
				unboundImports = new HashSet<Import>();
			}
			unboundImports.Add(import);
		}

		// Token: 0x060098DD RID: 39133 RVA: 0x001F9206 File Offset: 0x001F7406
		public static Assembly Assemble(IList<Module> modules, IEngineHost hostEnvironment, Action<IError> log)
		{
			return Linker.Assemble(Linker.Link(modules, log, LinkOptions.None), hostEnvironment, log);
		}

		// Token: 0x060098DE RID: 39134 RVA: 0x001F9218 File Offset: 0x001F7418
		public static Assembly Assemble(Module module, IEngineHost hostEnvironment, Action<IError> log)
		{
			Keys exportKeys = module.ExportKeys;
			Import[] imports = module.Imports;
			for (int i = 0; i < imports.Length; i++)
			{
				Import import = imports[i];
				foreach (SourceLocation sourceLocation in module.GetImportLocations(i))
				{
					if (import.Section != null)
					{
						log(SourceErrors.UnknownSection(sourceLocation, import.Section));
					}
					else
					{
						log(SourceErrors.UnknownIdentifier(sourceLocation, import.Section, import.Name));
					}
				}
			}
			Linker.AssemblyRecordValue assemblyRecordValue = new Linker.AssemblyRecordValue(exportKeys);
			RecordValue recordValue = module.Link(RecordValue.New(Linker.environmentKeys, new Value[]
			{
				assemblyRecordValue,
				RecordValue.Empty
			}), hostEnvironment);
			RecordValue asRecord = recordValue["Shared"].AsRecord;
			assemblyRecordValue.Initialize(asRecord);
			Value value = recordValue["Entry"];
			return new Linker.LinkedAssembly(asRecord, value.IsNull ? null : value.AsFunction, module);
		}

		// Token: 0x04005100 RID: 20736
		private static readonly Keys environmentKeys = Keys.New("Shared", "Sections");

		// Token: 0x04005101 RID: 20737
		public static readonly Keys LinkedKeys = Keys.New("Entry", "Shared", "Section");

		// Token: 0x0200179D RID: 6045
		private class LinkedModule : Module
		{
			// Token: 0x060098E0 RID: 39136 RVA: 0x001F9344 File Offset: 0x001F7544
			public LinkedModule(Import[] imports, Dictionary<string, int> sharedNames, Dictionary<string, int> sectionNames, IList<Module> modules, Module exportedModule, bool hideEnvironment)
			{
				this.imports = imports;
				this.sharedNameKeys = Keys.New(sharedNames.Keys.ToArray<string>());
				this.exportKeys = ((exportedModule != null) ? exportedModule.ExportKeys : this.sharedNameKeys);
				this.sectionKeys = ((exportedModule != null) ? exportedModule.SectionKeys : Keys.Empty);
				this.sectionNameKeys = ((sectionNames != null) ? Keys.New(sectionNames.Keys.ToArray<string>()) : Keys.Empty);
				this.sharedNames = sharedNames;
				this.sectionNames = sectionNames;
				this.modules = modules;
				this.exportedModule = exportedModule;
				this.hideEnvironment = hideEnvironment;
			}

			// Token: 0x1700278F RID: 10127
			// (get) Token: 0x060098E1 RID: 39137 RVA: 0x001F93EB File Offset: 0x001F75EB
			public override string Name
			{
				get
				{
					if (this.exportedModule == null)
					{
						return null;
					}
					return this.exportedModule.Name;
				}
			}

			// Token: 0x17002790 RID: 10128
			// (get) Token: 0x060098E2 RID: 39138 RVA: 0x00002105 File Offset: 0x00000305
			public override ModuleKind Kind
			{
				get
				{
					return ModuleKind.Module;
				}
			}

			// Token: 0x17002791 RID: 10129
			// (get) Token: 0x060098E3 RID: 39139 RVA: 0x001F9402 File Offset: 0x001F7602
			public override Import[] Imports
			{
				get
				{
					return this.imports;
				}
			}

			// Token: 0x17002792 RID: 10130
			// (get) Token: 0x060098E4 RID: 39140 RVA: 0x001F940A File Offset: 0x001F760A
			public override Keys ExportKeys
			{
				get
				{
					return this.exportKeys;
				}
			}

			// Token: 0x17002793 RID: 10131
			// (get) Token: 0x060098E5 RID: 39141 RVA: 0x001F9412 File Offset: 0x001F7612
			public override Keys SectionKeys
			{
				get
				{
					return this.sectionKeys;
				}
			}

			// Token: 0x17002794 RID: 10132
			// (get) Token: 0x060098E6 RID: 39142 RVA: 0x001F941A File Offset: 0x001F761A
			public override RecordValue Metadata
			{
				get
				{
					if (this.exportedModule == null)
					{
						return RecordValue.Empty;
					}
					return this.exportedModule.Metadata;
				}
			}

			// Token: 0x17002795 RID: 10133
			// (get) Token: 0x060098E7 RID: 39143 RVA: 0x001F9435 File Offset: 0x001F7635
			public override string Version
			{
				get
				{
					Module module = this.exportedModule;
					if (module == null)
					{
						return null;
					}
					return module.Version;
				}
			}

			// Token: 0x060098E8 RID: 39144 RVA: 0x001F9448 File Offset: 0x001F7648
			public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
			{
				RecordValue[] array = new RecordValue[this.modules.Count];
				Value value = (this.hideEnvironment ? RecordValue.Empty : environment["Shared"]);
				RecordValue sharedBindings = null;
				RecordValue recordValue = null;
				if (this.exportedModule != null || this.hideEnvironment)
				{
					value = value.Concatenate(RecordValue.New(this.sharedNameKeys, (int i) => sharedBindings[this.sharedNameKeys[i]]));
				}
				Value value2 = environment["Sections"];
				if (this.sectionNames != null)
				{
					value2 = value2.Concatenate(new Linker.LinkedModule.SectionRecordValue(this.sectionNameKeys, this.sectionNames, array));
				}
				if (this.exportedModule != null || this.sectionNames != null)
				{
					environment = RecordValue.New(Linker.environmentKeys, new Value[] { value, value2 });
				}
				for (int k = 0; k < this.modules.Count; k++)
				{
					array[k] = this.modules[k].Link(environment, hostEnvironment);
					if (this.modules[k] == this.exportedModule)
					{
						recordValue = array[k];
					}
				}
				Value value3 = Value.Null;
				foreach (RecordValue value4 in array)
				{
					if (!value4["Entry"].IsNull)
					{
						value3 = value4["Entry"];
					}
				}
				sharedBindings = new Linker.LinkedModule.SharedRecordValue(this.sharedNameKeys, this.sharedNames, array);
				RecordValue recordValue2;
				if ((recordValue2 = recordValue) == null)
				{
					recordValue2 = RecordValue.New(Linker.LinkedKeys, new Value[]
					{
						value3,
						sharedBindings,
						RecordValue.Empty
					});
				}
				return recordValue2;
			}

			// Token: 0x060098E9 RID: 39145 RVA: 0x001F95F0 File Offset: 0x001F77F0
			public override SourceLocation[] GetImportLocations(int importIndex)
			{
				List<SourceLocation> list = new List<SourceLocation>();
				foreach (Module module in this.modules)
				{
					int num = 0;
					Import[] array = module.Imports;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] == this.imports[importIndex])
						{
							list.AddRange(module.GetImportLocations(num));
						}
						num++;
					}
				}
				return list.ToArray();
			}

			// Token: 0x060098EA RID: 39146 RVA: 0x001F968C File Offset: 0x001F788C
			public override SourceLocation GetExportLocation(string name)
			{
				foreach (Module module in this.modules)
				{
					foreach (string text in module.ExportKeys)
					{
						if (name == text)
						{
							return module.GetExportLocation(name);
						}
					}
				}
				return SourceLocation.None;
			}

			// Token: 0x04005102 RID: 20738
			private readonly Import[] imports;

			// Token: 0x04005103 RID: 20739
			private readonly Keys exportKeys;

			// Token: 0x04005104 RID: 20740
			private readonly Keys sectionKeys;

			// Token: 0x04005105 RID: 20741
			private readonly Keys sectionNameKeys;

			// Token: 0x04005106 RID: 20742
			private readonly Keys sharedNameKeys;

			// Token: 0x04005107 RID: 20743
			private readonly Dictionary<string, int> sectionNames;

			// Token: 0x04005108 RID: 20744
			private readonly Dictionary<string, int> sharedNames;

			// Token: 0x04005109 RID: 20745
			private readonly IList<Module> modules;

			// Token: 0x0400510A RID: 20746
			private readonly Module exportedModule;

			// Token: 0x0400510B RID: 20747
			private readonly bool hideEnvironment;

			// Token: 0x0200179E RID: 6046
			private class SharedRecordValue : RecordValue
			{
				// Token: 0x060098EB RID: 39147 RVA: 0x001F972C File Offset: 0x001F792C
				public SharedRecordValue(Keys keys, Dictionary<string, int> sharedNames, RecordValue[] bindings)
				{
					this.keys = keys;
					this.sharedNames = sharedNames;
					this.bindings = bindings;
				}

				// Token: 0x17002796 RID: 10134
				// (get) Token: 0x060098EC RID: 39148 RVA: 0x001F9749 File Offset: 0x001F7949
				public override Keys Keys
				{
					get
					{
						return this.keys;
					}
				}

				// Token: 0x17002797 RID: 10135
				// (get) Token: 0x060098ED RID: 39149 RVA: 0x001F9751 File Offset: 0x001F7951
				public override TypeValue Type
				{
					get
					{
						if (this.type == null)
						{
							this.type = RecordTypeValue.New(this.keys);
						}
						return this.type;
					}
				}

				// Token: 0x060098EE RID: 39150 RVA: 0x001F9774 File Offset: 0x001F7974
				public override IValueReference GetReference(int index)
				{
					string text = this.keys[index];
					int num = this.sharedNames[text];
					if (num == -1)
					{
						return base.GetReference(index);
					}
					RecordValue asRecord = this.bindings[num]["Shared"].AsRecord;
					int num2 = asRecord.IndexOf(text);
					return asRecord.GetReference(num2);
				}

				// Token: 0x17002798 RID: 10136
				public override Value this[int index]
				{
					get
					{
						string text = this.keys[index];
						int num = this.sharedNames[text];
						if (num == -1)
						{
							throw ValueException.DuplicateExport(text, Value.Null);
						}
						return this.bindings[num]["Shared"][text];
					}
				}

				// Token: 0x0400510C RID: 20748
				private TypeValue type;

				// Token: 0x0400510D RID: 20749
				private Keys keys;

				// Token: 0x0400510E RID: 20750
				private Dictionary<string, int> sharedNames;

				// Token: 0x0400510F RID: 20751
				private RecordValue[] bindings;
			}

			// Token: 0x0200179F RID: 6047
			private class SectionRecordValue : RecordValue
			{
				// Token: 0x060098F0 RID: 39152 RVA: 0x001F981B File Offset: 0x001F7A1B
				public SectionRecordValue(Keys keys, Dictionary<string, int> sectionNames, RecordValue[] bindings)
				{
					this.keys = keys;
					this.sectionNames = sectionNames;
					this.bindings = bindings;
				}

				// Token: 0x17002799 RID: 10137
				// (get) Token: 0x060098F1 RID: 39153 RVA: 0x001F9838 File Offset: 0x001F7A38
				public override Keys Keys
				{
					get
					{
						return this.keys;
					}
				}

				// Token: 0x1700279A RID: 10138
				// (get) Token: 0x060098F2 RID: 39154 RVA: 0x001F9840 File Offset: 0x001F7A40
				public override TypeValue Type
				{
					get
					{
						if (this.type == null)
						{
							this.type = RecordTypeValue.New(this.keys);
						}
						return this.type;
					}
				}

				// Token: 0x1700279B RID: 10139
				public override Value this[int index]
				{
					get
					{
						string text = this.keys[index];
						return this.bindings[this.sectionNames[text]]["Section"];
					}
				}

				// Token: 0x04005110 RID: 20752
				private TypeValue type;

				// Token: 0x04005111 RID: 20753
				private Keys keys;

				// Token: 0x04005112 RID: 20754
				private Dictionary<string, int> sectionNames;

				// Token: 0x04005113 RID: 20755
				private RecordValue[] bindings;
			}
		}

		// Token: 0x020017A1 RID: 6049
		private class AssemblyRecordValue : RecordValue
		{
			// Token: 0x060098F6 RID: 39158 RVA: 0x001F98B9 File Offset: 0x001F7AB9
			public AssemblyRecordValue(Keys keys)
			{
				this.keys = keys;
			}

			// Token: 0x060098F7 RID: 39159 RVA: 0x001F98C8 File Offset: 0x001F7AC8
			public void Initialize(RecordValue value)
			{
				this.value = value;
			}

			// Token: 0x1700279C RID: 10140
			// (get) Token: 0x060098F8 RID: 39160 RVA: 0x001F98D1 File Offset: 0x001F7AD1
			public override Keys Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x1700279D RID: 10141
			// (get) Token: 0x060098F9 RID: 39161 RVA: 0x001F98D9 File Offset: 0x001F7AD9
			public override TypeValue Type
			{
				get
				{
					if (this.type == null)
					{
						this.type = RecordTypeValue.New(this.keys);
					}
					return this.type;
				}
			}

			// Token: 0x1700279E RID: 10142
			public override Value this[int index]
			{
				get
				{
					if (this.value == null)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ValueException_CyclicReference, null, null);
					}
					if (this.exports == null)
					{
						this.exports = new Value[this.keys.Length];
					}
					Value value = this.exports[index];
					if (value == null)
					{
						value = this.value[this.keys[index]];
						this.exports[index] = value;
					}
					return value;
				}
			}

			// Token: 0x060098FB RID: 39163 RVA: 0x001F996A File Offset: 0x001F7B6A
			public override IValueReference GetReference(int index)
			{
				return new Linker.AssemblyRecordValue.FieldReference(this, index);
			}

			// Token: 0x04005116 RID: 20758
			private TypeValue type;

			// Token: 0x04005117 RID: 20759
			private Keys keys;

			// Token: 0x04005118 RID: 20760
			private Value value;

			// Token: 0x04005119 RID: 20761
			private Value[] exports;

			// Token: 0x020017A2 RID: 6050
			private class FieldReference : IValueReference
			{
				// Token: 0x060098FC RID: 39164 RVA: 0x001F9973 File Offset: 0x001F7B73
				public FieldReference(Linker.AssemblyRecordValue record, int index)
				{
					this.record = record;
					this.index = index;
				}

				// Token: 0x1700279F RID: 10143
				// (get) Token: 0x060098FD RID: 39165 RVA: 0x001F9989 File Offset: 0x001F7B89
				public bool Evaluated
				{
					get
					{
						return this.record.exports != null && this.index < this.record.Count && this.record.exports[this.index] != null;
					}
				}

				// Token: 0x170027A0 RID: 10144
				// (get) Token: 0x060098FE RID: 39166 RVA: 0x001F99C2 File Offset: 0x001F7BC2
				public Value Value
				{
					get
					{
						return this.record[this.index];
					}
				}

				// Token: 0x0400511A RID: 20762
				private readonly Linker.AssemblyRecordValue record;

				// Token: 0x0400511B RID: 20763
				private readonly int index;
			}
		}

		// Token: 0x020017A3 RID: 6051
		private class LinkedAssembly : Assembly
		{
			// Token: 0x060098FF RID: 39167 RVA: 0x001F99D5 File Offset: 0x001F7BD5
			public LinkedAssembly(RecordValue exports, FunctionValue function, Module module)
			{
				this.exports = exports;
				this.function = function;
				this.module = module;
			}

			// Token: 0x170027A1 RID: 10145
			// (get) Token: 0x06009900 RID: 39168 RVA: 0x001F99F2 File Offset: 0x001F7BF2
			public override RecordValue Exports
			{
				get
				{
					return this.exports;
				}
			}

			// Token: 0x170027A2 RID: 10146
			// (get) Token: 0x06009901 RID: 39169 RVA: 0x001F99FA File Offset: 0x001F7BFA
			public override FunctionValue Function
			{
				get
				{
					return this.function;
				}
			}

			// Token: 0x0400511C RID: 20764
			private RecordValue exports;

			// Token: 0x0400511D RID: 20765
			private Module module;

			// Token: 0x0400511E RID: 20766
			private FunctionValue function;
		}
	}
}
