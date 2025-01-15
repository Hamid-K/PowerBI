using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B1 RID: 177
	public sealed class MiningAttributeCollection : ICollection, IEnumerable
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x0002A545 File Offset: 0x00028745
		internal MiningAttributeCollection(MiningModel parentModel)
		{
			this.arAttributesInternal = new ArrayList();
			this.PopulateCollection(parentModel);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002A55F File Offset: 0x0002875F
		internal MiningAttributeCollection(MiningModel parentModel, MiningFeatureSelection filter)
		{
			this.arAttributesInternal = new ArrayList();
			this.FilterCollection(parentModel.Attributes, filter);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002A580 File Offset: 0x00028780
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

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002A65C File Offset: 0x0002885C
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

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002A808 File Offset: 0x00028A08
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

		// Token: 0x1700034C RID: 844
		public MiningAttribute this[int index]
		{
			get
			{
				return (MiningAttribute)this.arAttributesInternal[index];
			}
		}

		// Token: 0x1700034D RID: 845
		public MiningAttribute this[string index]
		{
			get
			{
				return this.Find(index);
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002A8E9 File Offset: 0x00028AE9
		public MiningAttribute Find(string index)
		{
			return null;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002A8EC File Offset: 0x00028AEC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002A8EF File Offset: 0x00028AEF
		public object SyncRoot
		{
			get
			{
				return this.arAttributesInternal.SyncRoot;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002A8FC File Offset: 0x00028AFC
		public int Count
		{
			get
			{
				return this.arAttributesInternal.Count;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002A909 File Offset: 0x00028B09
		public void CopyTo(MiningAttribute[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002A914 File Offset: 0x00028B14
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002A94F File Offset: 0x00028B4F
		public MiningAttributeCollection.Enumerator GetEnumerator()
		{
			return new MiningAttributeCollection.Enumerator(this);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002A957 File Offset: 0x00028B57
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400069F RID: 1695
		private ArrayList arAttributesInternal;

		// Token: 0x040006A0 RID: 1696
		internal Hashtable hashAttrIDs;

		// Token: 0x040006A1 RID: 1697
		internal static string attribQueryStmt = "CALL System.GetModelAttributes('{0}')";

		// Token: 0x040006A2 RID: 1698
		internal static int attIdIndex = 0;

		// Token: 0x040006A3 RID: 1699
		internal static int nameIndex = 1;

		// Token: 0x040006A4 RID: 1700
		internal static int shortNameIndex = 2;

		// Token: 0x040006A5 RID: 1701
		internal static int isInputIndex = 3;

		// Token: 0x040006A6 RID: 1702
		internal static int isPredictableIndex = 4;

		// Token: 0x040006A7 RID: 1703
		internal static int featureSelectionIndex = 5;

		// Token: 0x040006A8 RID: 1704
		internal static int keyColumnIndex = 6;

		// Token: 0x040006A9 RID: 1705
		internal static int valueColumnIndex = 7;

		// Token: 0x020001B6 RID: 438
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600134A RID: 4938 RVA: 0x000444AC File Offset: 0x000426AC
			internal Enumerator(MiningAttributeCollection miningModelAttributes)
			{
				this.currentIndex = -1;
				this.attributes = miningModelAttributes;
			}

			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x0600134B RID: 4939 RVA: 0x000444BC File Offset: 0x000426BC
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

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x0600134C RID: 4940 RVA: 0x000444F8 File Offset: 0x000426F8
			object IEnumerator.Current
			{
				get
				{
					return this.attributes[this.currentIndex];
				}
			}

			// Token: 0x0600134D RID: 4941 RVA: 0x0004450C File Offset: 0x0004270C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.attributes.Count;
			}

			// Token: 0x0600134E RID: 4942 RVA: 0x00044537 File Offset: 0x00042737
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CDA RID: 3290
			private int currentIndex;

			// Token: 0x04000CDB RID: 3291
			private MiningAttributeCollection attributes;
		}
	}
}
