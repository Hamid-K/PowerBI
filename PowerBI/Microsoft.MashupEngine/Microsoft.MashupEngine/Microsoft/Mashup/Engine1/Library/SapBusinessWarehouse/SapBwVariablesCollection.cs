using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x02000501 RID: 1281
	internal sealed class SapBwVariablesCollection : IEnumerable<SapBwVariable>, IEnumerable
	{
		// Token: 0x060029C3 RID: 10691 RVA: 0x0007CE73 File Offset: 0x0007B073
		public SapBwVariablesCollection(ISapBwService service, string catalogName, SapBwMdxCube cube)
		{
			this.service = service;
			this.catalogName = catalogName;
			this.cube = cube;
		}

		// Token: 0x17001006 RID: 4102
		public SapBwVariable this[string name]
		{
			get
			{
				this.EnsureInitialized();
				return this.variablesByName[name];
			}
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x0007CEA4 File Offset: 0x0007B0A4
		public IEnumerator<SapBwVariable> GetEnumerator()
		{
			this.EnsureInitialized();
			return this.variablesByName.Values.OrderBy((SapBwVariable v) => v.Ordinal).GetEnumerator();
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x0007CEE0 File Offset: 0x0007B0E0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x0007CEE8 File Offset: 0x0007B0E8
		private void EnsureInitialized()
		{
			if (this.variablesByName == null)
			{
				Dictionary<string, SapBwVariable> dictionary = new Dictionary<string, SapBwVariable>();
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
				using (IDataReader dataReader = this.service.ExtractMetadata("BAPI_MDPROVIDER_GET_VARIABLES", "VARIABLES", new SapBwRestrictions
				{
					{ "CAT_NAM", this.catalogName },
					{
						"CUBE_NAM",
						this.cube.Name
					}
				}))
				{
					while (dataReader.Read())
					{
						string @string = dataReader.GetString(2);
						SapBwVariableSelectionType sapBwVariableSelectionType = (SapBwVariableSelectionType)int.Parse(dataReader.GetString(10), CultureInfo.InvariantCulture);
						SapBwDataType sapBwDataType = null;
						string text = dataReader[7] as string;
						if (text != null)
						{
							SapBwDataType.TryGetByName(text, out sapBwDataType);
						}
						dictionary2.Add(dataReader.GetString(4), @string);
						dictionary.Add(@string, new SapBwVariable
						{
							Caption = (dataReader[3] as string),
							DataType = sapBwDataType,
							DefaultHigh = dataReader[15],
							DefaultHighValueCaption = (dataReader[17] as string),
							DefaultLow = dataReader[14],
							DefaultLowValueCaption = (dataReader[16] as string),
							Description = (dataReader[18] as string),
							Dimension = (dataReader[12] as string),
							EntryType = (SapBwVariableEntryType)int.Parse(dataReader.GetString(11), CultureInfo.InvariantCulture),
							Hierarchy = (dataReader[13] as string),
							MdxIdentifier = @string,
							Ordinal = int.Parse(dataReader.GetString(5), CultureInfo.InvariantCulture),
							SelectionType = sapBwVariableSelectionType,
							Type = SapBwVariablesCollection.GetTypeFromGetVariablesBapi((SapBwVariableTypeFromBapi)int.Parse(dataReader.GetString(6), CultureInfo.InvariantCulture), dataReader[12] as string, dataReader[13] as string)
						});
					}
				}
				if (dictionary.Count > 0)
				{
					ILookup<string, SapBwVariable> lookup = this.service.GroupVariablesForAdditionalMetadata(this.cube, dictionary);
					string[] array = lookup.Select((IGrouping<string, SapBwVariable> g) => g.Key).ToArray<string>();
					IDataReaderWithTableSchema dataReaderWithTableSchema;
					if (array.Length != 0 && this.service.TryGetInfoObjectsDetail(array, out dataReaderWithTableSchema))
					{
						using (dataReaderWithTableSchema)
						{
							while (dataReaderWithTableSchema.Read())
							{
								string text2 = ((dataReaderWithTableSchema.FieldCount > 17) ? dataReaderWithTableSchema.GetString(17) : dataReaderWithTableSchema.GetString(0));
								if (!string.IsNullOrEmpty(text2))
								{
									foreach (SapBwVariable sapBwVariable in lookup[text2])
									{
										sapBwVariable.ApplyInfo(dataReaderWithTableSchema, true);
									}
								}
							}
						}
					}
				}
				this.variablesByName = dictionary;
			}
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x0007D200 File Offset: 0x0007B400
		private static SapBwVariableType GetTypeFromGetVariablesBapi(SapBwVariableTypeFromBapi variableType, string dimension, string hierarchy)
		{
			switch (variableType)
			{
			case SapBwVariableTypeFromBapi.Member:
				if (dimension != null && hierarchy != null && dimension.Equals(hierarchy, StringComparison.Ordinal))
				{
					return SapBwVariableType.CharacteristicValue;
				}
				return SapBwVariableType.HierarchyNode;
			case SapBwVariableTypeFromBapi.Numeric:
				if (dimension != null && dimension.Equals("[1FORMULA]", StringComparison.Ordinal))
				{
					return SapBwVariableType.Formula;
				}
				return SapBwVariableType.Text;
			case SapBwVariableTypeFromBapi.Hierarchy:
				return SapBwVariableType.Hierarchy;
			default:
				return SapBwVariableType.CharacteristicValue;
			}
		}

		// Token: 0x04001228 RID: 4648
		private readonly ISapBwService service;

		// Token: 0x04001229 RID: 4649
		private readonly string catalogName;

		// Token: 0x0400122A RID: 4650
		private readonly SapBwMdxCube cube;

		// Token: 0x0400122B RID: 4651
		private Dictionary<string, SapBwVariable> variablesByName;
	}
}
