using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001791 RID: 6033
	internal sealed class LanguageLibraryModule : Module
	{
		// Token: 0x17002786 RID: 10118
		// (get) Token: 0x060098B0 RID: 39088 RVA: 0x001F7F5E File Offset: 0x001F615E
		public override string Name
		{
			get
			{
				return "LanguageLibraryModule";
			}
		}

		// Token: 0x17002787 RID: 10119
		// (get) Token: 0x060098B1 RID: 39089 RVA: 0x001F7F65 File Offset: 0x001F6165
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(10, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "List.Count";
						case 1:
							return "List.Distinct";
						case 2:
							return "List.FirstN";
						case 3:
							return "List.IsEmpty";
						case 4:
							return "List.LastN";
						case 5:
							return "List.Select";
						case 6:
							return "List.Skip";
						case 7:
							return "List.Sort";
						case 8:
							return "List.Transform";
						case 9:
							return "List.TransformMany";
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x060098B2 RID: 39090 RVA: 0x001F7FA1 File Offset: 0x001F61A1
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			if (this.exports == null)
			{
				this.exports = RecordValue.New(this.ExportKeys, delegate(int index)
				{
					switch (index)
					{
					case 0:
						return LanguageLibrary.List.Count;
					case 1:
						return LanguageLibrary.List.Distinct;
					case 2:
						return LanguageLibrary.List.FirstN;
					case 3:
						return LanguageLibrary.List.IsEmpty;
					case 4:
						return LanguageLibrary.List.LastN;
					case 5:
						return LanguageLibrary.List.Select;
					case 6:
						return LanguageLibrary.List.Skip;
					case 7:
						return LanguageLibrary.List.Sort;
					case 8:
						return LanguageLibrary.List.Transform;
					case 9:
						return LanguageLibrary.List.TransformMany;
					default:
						throw new InvalidOperationException();
					}
				});
			}
			return this.exports;
		}

		// Token: 0x040050D8 RID: 20696
		private Keys exportKeys;

		// Token: 0x040050D9 RID: 20697
		private RecordValue exports;

		// Token: 0x02001792 RID: 6034
		private enum Exports
		{
			// Token: 0x040050DB RID: 20699
			List_Count,
			// Token: 0x040050DC RID: 20700
			List_Distinct,
			// Token: 0x040050DD RID: 20701
			List_FirstN,
			// Token: 0x040050DE RID: 20702
			List_IsEmpty,
			// Token: 0x040050DF RID: 20703
			List_LastN,
			// Token: 0x040050E0 RID: 20704
			List_Select,
			// Token: 0x040050E1 RID: 20705
			List_Skip,
			// Token: 0x040050E2 RID: 20706
			List_Sort,
			// Token: 0x040050E3 RID: 20707
			List_Transform,
			// Token: 0x040050E4 RID: 20708
			List_TransformMany,
			// Token: 0x040050E5 RID: 20709
			Count
		}
	}
}
