using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000991 RID: 2449
	internal sealed class MdxDisplayFolderBuilder : MdxDisplayFolderContainerBuilder
	{
		// Token: 0x0600463D RID: 17981 RVA: 0x000EBCEC File Offset: 0x000E9EEC
		public MdxDisplayFolderBuilder()
			: base(false)
		{
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x000EBCF8 File Offset: 0x000E9EF8
		public int Count
		{
			get
			{
				int num = 0;
				if (this.subfolders != null)
				{
					num += this.subfolders.Count;
				}
				if (this.values != null)
				{
					num += this.values.Count;
				}
				if (this.hierarchies != null)
				{
					num += this.hierarchies.Count;
				}
				return num;
			}
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x000EBD4C File Offset: 0x000E9F4C
		public override void AddValue(string identity, string name, TextValue kind, Value value)
		{
			if (this.values == null)
			{
				this.values = new List<MdxDisplayFolderBuilder.DisplayFolderValue>();
			}
			this.values.Add(new MdxDisplayFolderBuilder.DisplayFolderValue
			{
				Id = identity,
				Name = name,
				Kind = kind,
				Value = value
			});
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x000EBD9C File Offset: 0x000E9F9C
		public override void AddHierarchy(string id, string name, TableValue hierarchyFolder)
		{
			if (this.hierarchies == null)
			{
				this.hierarchies = new List<MdxDisplayFolderBuilder.DisplayFolderValue>();
			}
			this.hierarchies.Add(new MdxDisplayFolderBuilder.DisplayFolderValue
			{
				Id = id,
				Name = name,
				Kind = CubeObjectTableBuilder.DimensionHierarchyFolderKind,
				Value = hierarchyFolder
			});
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x000EBDEC File Offset: 0x000E9FEC
		public void AddKpiMeasure(MdxMeasure kpiMeasure, string caption)
		{
			string mdxIdentifier = kpiMeasure.MdxIdentifier;
			MeasureValue measureValue = new MeasureValue(new IdentifierCubeExpression(mdxIdentifier), TypeValue.Any);
			this.AddValue(mdxIdentifier, caption, CubeObjectTableBuilder.MeasureKind, measureValue);
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x000EBE20 File Offset: 0x000EA020
		public MdxDisplayFolderContainerBuilder CreateSubfoldersFromPaths(string displayFolders, TextValue kind, bool supportsProperties)
		{
			string[] array = displayFolders.Split(new char[] { ';' });
			MdxDisplayFolderBuilder[] array2 = new MdxDisplayFolderBuilder[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.CreateSubfolderFromPath(array[i], kind);
			}
			return new MdxDisplayFolderBuilder.DisplayFolderCollection(supportsProperties, array2);
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x000EBE6C File Offset: 0x000EA06C
		public MdxDisplayFolderBuilder CreateSubfolderFromPath(string path, TextValue kind)
		{
			if (string.IsNullOrEmpty(path))
			{
				return this;
			}
			string[] array = path.Split(new char[] { '\\' });
			MdxDisplayFolderBuilder mdxDisplayFolderBuilder = this;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				TextValue textValue = TextValue.New(array2[i]);
				mdxDisplayFolderBuilder = MdxDisplayFolderBuilder.CreateSubfolder(mdxDisplayFolderBuilder, textValue, textValue, kind);
			}
			return mdxDisplayFolderBuilder;
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x000EBEB9 File Offset: 0x000EA0B9
		public MdxDisplayFolderBuilder CreateSubfolder(TextValue id, TextValue name, TextValue kind)
		{
			return MdxDisplayFolderBuilder.CreateSubfolder(this, id, name, kind);
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x000EBEC4 File Offset: 0x000EA0C4
		private static MdxDisplayFolderBuilder CreateSubfolder(MdxDisplayFolderBuilder displayFolder, TextValue id, TextValue name, TextValue kind)
		{
			if (displayFolder.subfolders == null)
			{
				displayFolder.subfolders = new Dictionary<string, MdxDisplayFolderBuilder.SubFolder>();
			}
			MdxDisplayFolderBuilder.SubFolder subFolder;
			if (!displayFolder.subfolders.TryGetValue(id.AsString, out subFolder))
			{
				subFolder = new MdxDisplayFolderBuilder.SubFolder(id, name, kind);
				displayFolder.subfolders[id.AsString] = subFolder;
			}
			return subFolder.DisplayFolder;
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x000EBF1C File Offset: 0x000EA11C
		public void Populate(CubeObjectTableBuilder builder)
		{
			if (this.subfolders != null)
			{
				foreach (MdxDisplayFolderBuilder.SubFolder subFolder in this.subfolders.Values.OrderBy((MdxDisplayFolderBuilder.SubFolder kv) => kv.Name.AsString))
				{
					CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
					subFolder.DisplayFolder.Populate(cubeObjectTableBuilder);
					builder.AddObject(subFolder.Id, subFolder.Name, TextValue.Empty, subFolder.Kind, cubeObjectTableBuilder.ToTable());
				}
			}
			if (this.values != null)
			{
				foreach (MdxDisplayFolderBuilder.DisplayFolderValue displayFolderValue in this.values)
				{
					builder.AddObject(displayFolderValue.Id, displayFolderValue.Name, displayFolderValue.Kind, displayFolderValue.Value);
				}
			}
			if (this.hierarchies != null)
			{
				foreach (MdxDisplayFolderBuilder.DisplayFolderValue displayFolderValue2 in this.hierarchies)
				{
					builder.AddDimensionHierarchyFolder(displayFolderValue2.Id, displayFolderValue2.Name, displayFolderValue2.Value);
				}
			}
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x000EC090 File Offset: 0x000EA290
		public TableValue ToTable()
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			this.Populate(cubeObjectTableBuilder);
			return cubeObjectTableBuilder.ToTable();
		}

		// Token: 0x04002526 RID: 9510
		private Dictionary<string, MdxDisplayFolderBuilder.SubFolder> subfolders;

		// Token: 0x04002527 RID: 9511
		private List<MdxDisplayFolderBuilder.DisplayFolderValue> values;

		// Token: 0x04002528 RID: 9512
		private List<MdxDisplayFolderBuilder.DisplayFolderValue> hierarchies;

		// Token: 0x02000992 RID: 2450
		private class DisplayFolderValue
		{
			// Token: 0x04002529 RID: 9513
			public string Id;

			// Token: 0x0400252A RID: 9514
			public string Name;

			// Token: 0x0400252B RID: 9515
			public TextValue Kind;

			// Token: 0x0400252C RID: 9516
			public Value Value;
		}

		// Token: 0x02000993 RID: 2451
		private class SubFolder
		{
			// Token: 0x06004649 RID: 17993 RVA: 0x000EC0B0 File Offset: 0x000EA2B0
			public SubFolder(TextValue id, TextValue name, TextValue kind)
			{
				this.displayFolder = new MdxDisplayFolderBuilder();
				this.id = id;
				this.name = name;
				this.kind = kind;
			}

			// Token: 0x17001667 RID: 5735
			// (get) Token: 0x0600464A RID: 17994 RVA: 0x000EC0D8 File Offset: 0x000EA2D8
			public MdxDisplayFolderBuilder DisplayFolder
			{
				get
				{
					return this.displayFolder;
				}
			}

			// Token: 0x17001668 RID: 5736
			// (get) Token: 0x0600464B RID: 17995 RVA: 0x000EC0E0 File Offset: 0x000EA2E0
			public TextValue Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x17001669 RID: 5737
			// (get) Token: 0x0600464C RID: 17996 RVA: 0x000EC0E8 File Offset: 0x000EA2E8
			public TextValue Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700166A RID: 5738
			// (get) Token: 0x0600464D RID: 17997 RVA: 0x000EC0F0 File Offset: 0x000EA2F0
			public TextValue Kind
			{
				get
				{
					return this.kind;
				}
			}

			// Token: 0x0400252D RID: 9517
			private readonly MdxDisplayFolderBuilder displayFolder;

			// Token: 0x0400252E RID: 9518
			private readonly TextValue id;

			// Token: 0x0400252F RID: 9519
			private readonly TextValue name;

			// Token: 0x04002530 RID: 9520
			private readonly TextValue kind;
		}

		// Token: 0x02000994 RID: 2452
		private class DisplayFolderCollection : MdxDisplayFolderContainerBuilder
		{
			// Token: 0x0600464E RID: 17998 RVA: 0x000EC0F8 File Offset: 0x000EA2F8
			public DisplayFolderCollection(bool supportsProperties, IList<MdxDisplayFolderBuilder> displayFolders)
				: base(supportsProperties)
			{
				this.displayFolders = displayFolders;
			}

			// Token: 0x0600464F RID: 17999 RVA: 0x000EC108 File Offset: 0x000EA308
			public override void AddValue(string id, string name, TextValue kind, Value value)
			{
				foreach (MdxDisplayFolderBuilder mdxDisplayFolderBuilder in this.displayFolders)
				{
					mdxDisplayFolderBuilder.AddValue(id, name, kind, value);
				}
			}

			// Token: 0x06004650 RID: 18000 RVA: 0x000EC158 File Offset: 0x000EA358
			public override void AddHierarchy(string id, string name, TableValue hierarchyFolder)
			{
				foreach (MdxDisplayFolderBuilder mdxDisplayFolderBuilder in this.displayFolders)
				{
					mdxDisplayFolderBuilder.AddHierarchy(id, name, hierarchyFolder);
				}
			}

			// Token: 0x04002531 RID: 9521
			private readonly IList<MdxDisplayFolderBuilder> displayFolders;
		}
	}
}
