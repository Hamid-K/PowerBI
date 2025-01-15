using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B1 RID: 177
	public sealed class MiningAttributeCollection : ICollection, IEnumerable
	{
		// Token: 0x060009F6 RID: 2550 RVA: 0x0002A215 File Offset: 0x00028415
		internal MiningAttributeCollection(MiningModel parentModel)
		{
			this.arAttributesInternal = new ArrayList();
			this.PopulateCollection(parentModel);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002A22F File Offset: 0x0002842F
		internal MiningAttributeCollection(MiningModel parentModel, MiningFeatureSelection filter)
		{
			this.arAttributesInternal = new ArrayList();
			this.FilterCollection(parentModel.Attributes, filter);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002A250 File Offset: 0x00028450
		private MiningModelColumn ModelColumnFromName(string colName, MiningModel parentModel)
		{
			MiningModelColumn miningModelColumn = null;
			if (colName == null || colName.Length == 0)
			{
				return miningModelColumn;
			}
			int num = colName.IndexOf(".", StringComparison.OrdinalIgnoreCase);
			string text = string.Empty;
			string text2 = string.Empty;
			if (num < 0)
			{
				text = colName;
				if (text.IndexOf("[", StringComparison.OrdinalIgnoreCase) == 0)
				{
					text = text.Substring(1, text.Length - 2);
				}
				miningModelColumn = parentModel.Columns[text];
			}
			else
			{
				text = colName.Substring(0, num);
				text2 = colName.Substring(num + 1, colName.Length - num - 1);
				if (text.IndexOf("[", StringComparison.OrdinalIgnoreCase) == 0)
				{
					text = text.Substring(1, text.Length - 2);
				}
				MiningModelColumn miningModelColumn2 = parentModel.Columns[text];
				if (text2.IndexOf("[", StringComparison.OrdinalIgnoreCase) == 0)
				{
					text2 = text2.Substring(1, text2.Length - 2);
				}
				miningModelColumn = miningModelColumn2.Columns[text2];
			}
			return miningModelColumn;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002A32C File Offset: 0x0002852C
		private void PopulateCollection(MiningModel parentModel)
		{
			this.hashAttrIDs = new Hashtable();
			AdomdCommand adomdCommand = new AdomdCommand();
			adomdCommand.Connection = parentModel.ParentConnection;
			int num = 0;
			foreach (MiningModelColumn miningModelColumn in parentModel.Columns)
			{
				if (miningModelColumn.IsTable)
				{
					foreach (MiningModelColumn miningModelColumn2 in miningModelColumn.Columns)
					{
						num++;
					}
				}
			}
			adomdCommand.CommandText = string.Format(CultureInfo.InvariantCulture, MiningAttributeCollection.attribQueryStmt, parentModel.Name);
			AdomdDataReader adomdDataReader = adomdCommand.ExecuteReader(CommandBehavior.SequentialAccess);
			while (adomdDataReader.Read())
			{
				int @int = adomdDataReader.GetInt32(MiningAttributeCollection.attIdIndex);
				string @string = adomdDataReader.GetString(MiningAttributeCollection.nameIndex);
				string string2 = adomdDataReader.GetString(MiningAttributeCollection.shortNameIndex);
				bool boolean = adomdDataReader.GetBoolean(MiningAttributeCollection.isInputIndex);
				bool boolean2 = adomdDataReader.GetBoolean(MiningAttributeCollection.isPredictableIndex);
				int int2 = adomdDataReader.GetInt32(MiningAttributeCollection.featureSelectionIndex);
				string string3 = adomdDataReader.GetString(MiningAttributeCollection.keyColumnIndex);
				string string4 = adomdDataReader.GetString(MiningAttributeCollection.valueColumnIndex);
				MiningAttribute miningAttribute = new MiningAttribute(parentModel);
				miningAttribute.attributeID = @int;
				miningAttribute.name = @string;
				miningAttribute.shortName = string2;
				miningAttribute.isInput = boolean;
				miningAttribute.isPredictable = boolean2;
				miningAttribute.featureSelection = (MiningFeatureSelection)int2;
				miningAttribute.keyColumn = this.ModelColumnFromName(string3, parentModel);
				miningAttribute.valueColumn = this.ModelColumnFromName(string4, parentModel);
				this.hashAttrIDs.Add(miningAttribute.name, miningAttribute.attributeID);
				this.arAttributesInternal.Add(miningAttribute);
			}
			adomdDataReader.Close();
			adomdCommand.Dispose();
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002A4D8 File Offset: 0x000286D8
		private void FilterCollection(MiningAttributeCollection parentCollection, MiningFeatureSelection filter)
		{
			foreach (MiningAttribute miningAttribute in parentCollection)
			{
				bool flag = false;
				switch (filter)
				{
				case MiningFeatureSelection.All:
					flag = true;
					break;
				case MiningFeatureSelection.NotSelected:
					flag = miningAttribute.FeatureSelection == MiningFeatureSelection.NotSelected;
					break;
				case MiningFeatureSelection.Selected:
					flag = miningAttribute.FeatureSelection == MiningFeatureSelection.Input || miningAttribute.FeatureSelection == MiningFeatureSelection.Output || miningAttribute.FeatureSelection == MiningFeatureSelection.InputAndOutput;
					break;
				case (MiningFeatureSelection)3:
				case (MiningFeatureSelection)5:
				case (MiningFeatureSelection)6:
				case (MiningFeatureSelection)7:
					break;
				case MiningFeatureSelection.Input:
					flag = miningAttribute.FeatureSelection == MiningFeatureSelection.Input;
					break;
				case MiningFeatureSelection.Output:
					flag = miningAttribute.FeatureSelection == MiningFeatureSelection.Output;
					break;
				default:
					if (filter == MiningFeatureSelection.InputAndOutput)
					{
						flag = miningAttribute.FeatureSelection == MiningFeatureSelection.InputAndOutput;
					}
					break;
				}
				if (flag)
				{
					this.arAttributesInternal.Add(miningAttribute);
				}
			}
		}

		// Token: 0x17000346 RID: 838
		public MiningAttribute this[int index]
		{
			get
			{
				return (MiningAttribute)this.arAttributesInternal[index];
			}
		}

		// Token: 0x17000347 RID: 839
		public MiningAttribute this[string index]
		{
			get
			{
				return this.Find(index);
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002A5B9 File Offset: 0x000287B9
		public MiningAttribute Find(string index)
		{
			return null;
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002A5BC File Offset: 0x000287BC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002A5BF File Offset: 0x000287BF
		public object SyncRoot
		{
			get
			{
				return this.arAttributesInternal.SyncRoot;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002A5CC File Offset: 0x000287CC
		public int Count
		{
			get
			{
				return this.arAttributesInternal.Count;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002A5D9 File Offset: 0x000287D9
		public void CopyTo(MiningAttribute[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002A5E4 File Offset: 0x000287E4
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002A61F File Offset: 0x0002881F
		public MiningAttributeCollection.Enumerator GetEnumerator()
		{
			return new MiningAttributeCollection.Enumerator(this);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002A627 File Offset: 0x00028827
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000692 RID: 1682
		private ArrayList arAttributesInternal;

		// Token: 0x04000693 RID: 1683
		internal Hashtable hashAttrIDs;

		// Token: 0x04000694 RID: 1684
		internal static string attribQueryStmt = "CALL System.GetModelAttributes('{0}')";

		// Token: 0x04000695 RID: 1685
		internal static int attIdIndex = 0;

		// Token: 0x04000696 RID: 1686
		internal static int nameIndex = 1;

		// Token: 0x04000697 RID: 1687
		internal static int shortNameIndex = 2;

		// Token: 0x04000698 RID: 1688
		internal static int isInputIndex = 3;

		// Token: 0x04000699 RID: 1689
		internal static int isPredictableIndex = 4;

		// Token: 0x0400069A RID: 1690
		internal static int featureSelectionIndex = 5;

		// Token: 0x0400069B RID: 1691
		internal static int keyColumnIndex = 6;

		// Token: 0x0400069C RID: 1692
		internal static int valueColumnIndex = 7;

		// Token: 0x020001B6 RID: 438
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600133D RID: 4925 RVA: 0x00043F70 File Offset: 0x00042170
			internal Enumerator(MiningAttributeCollection miningModelAttributes)
			{
				this.currentIndex = -1;
				this.attributes = miningModelAttributes;
			}

			// Token: 0x170006B7 RID: 1719
			// (get) Token: 0x0600133E RID: 4926 RVA: 0x00043F80 File Offset: 0x00042180
			public MiningAttribute Current
			{
				get
				{
					MiningAttribute miningAttribute;
					try
					{
						miningAttribute = this.attributes[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningAttribute;
				}
			}

			// Token: 0x170006B8 RID: 1720
			// (get) Token: 0x0600133F RID: 4927 RVA: 0x00043FBC File Offset: 0x000421BC
			object IEnumerator.Current
			{
				get
				{
					return this.attributes[this.currentIndex];
				}
			}

			// Token: 0x06001340 RID: 4928 RVA: 0x00043FD0 File Offset: 0x000421D0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.attributes.Count;
			}

			// Token: 0x06001341 RID: 4929 RVA: 0x00043FFB File Offset: 0x000421FB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CC9 RID: 3273
			private int currentIndex;

			// Token: 0x04000CCA RID: 3274
			private MiningAttributeCollection attributes;
		}
	}
}
