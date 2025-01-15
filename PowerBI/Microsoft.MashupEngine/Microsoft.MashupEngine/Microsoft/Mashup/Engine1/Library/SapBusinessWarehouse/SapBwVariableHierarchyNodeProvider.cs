using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004F7 RID: 1271
	internal abstract class SapBwVariableHierarchyNodeProvider : SapBwVariableValueProvider
	{
		// Token: 0x0600297E RID: 10622 RVA: 0x0007BE8F File Offset: 0x0007A08F
		protected SapBwVariableHierarchyNodeProvider(ISapBwService service, SapBwMdxCube mdxCube, SapBwVariable variable, bool allowNonAssigned)
			: base(service, mdxCube, variable, allowNonAssigned)
		{
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x0007BE9C File Offset: 0x0007A09C
		public bool HasMultipleHierarchies
		{
			get
			{
				this.EnsureInitialized();
				return this.hasMultipleHierarchies;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x0007BEAA File Offset: 0x0007A0AA
		public string SingleHierarchyUniqueName
		{
			get
			{
				this.EnsureInitialized();
				return this.singleHierarchyUniqueName;
			}
		}

		// Token: 0x06002981 RID: 10625
		protected abstract Dictionary<string, MdxHierarchy> GetExternalHierarchies(SapBwVariable variable);

		// Token: 0x06002982 RID: 10626
		protected abstract void AddValueProvider(MdxHierarchy defaultHierarcy, Dictionary<string, MdxHierarchy> hierarchyNames);

		// Token: 0x06002983 RID: 10627
		protected abstract void EnsureInitialized();

		// Token: 0x06002984 RID: 10628 RVA: 0x0007BEB8 File Offset: 0x0007A0B8
		protected void BuildValueProviders()
		{
			string text;
			Dictionary<string, MdxHierarchy> hierarchies = this.GetHierarchies(base.Variable.Hierarchy, out text);
			if (hierarchies != null && hierarchies.Count > 0)
			{
				SapBwHierarchyNodeVariableInfo sapBwHierarchyNodeVariableInfo = SapBwHierarchyNodeVariableInfo.New(hierarchies.Values, text);
				if (sapBwHierarchyNodeVariableInfo.FoundDefaultHierarchy && sapBwHierarchyNodeVariableInfo.ValueProviderHierarchy != null)
				{
					this.singleHierarchyUniqueName = text;
					this.AddValueProvider(sapBwHierarchyNodeVariableInfo.ValueProviderHierarchy, null);
				}
				else if (sapBwHierarchyNodeVariableInfo.FirstHierarchyUniqueName != null)
				{
					this.singleHierarchyUniqueName = sapBwHierarchyNodeVariableInfo.FirstHierarchyUniqueName;
				}
				Dictionary<string, MdxHierarchy> dictionary = new Dictionary<string, MdxHierarchy>();
				foreach (Tuple<string, MdxHierarchy> tuple in sapBwHierarchyNodeVariableInfo.HierarchyNames)
				{
					dictionary[tuple.Item1] = tuple.Item2;
				}
				if (dictionary.Count > 0)
				{
					this.AddValueProvider(null, dictionary);
				}
				this.hasMultipleHierarchies = sapBwHierarchyNodeVariableInfo.HasMultipleHierarchies;
				if (this.hasMultipleHierarchies)
				{
					this.singleHierarchyUniqueName = null;
				}
			}
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0007BFBC File Offset: 0x0007A1BC
		protected virtual Dictionary<string, MdxHierarchy> GetHierarchies(string variableHierarchy, out string defaultHierarchyUniqueName)
		{
			return SapBwVariableHierarchyNodeProvider.GetHierarchies(base.Variable, base.MdxCube, variableHierarchy, this.GetExternalHierarchies(base.Variable), out defaultHierarchyUniqueName);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0007BFE0 File Offset: 0x0007A1E0
		public static Dictionary<string, MdxHierarchy> GetHierarchies(SapBwVariable variable, SapBwMdxCube mdxCube, string variableHierarchy, Dictionary<string, MdxHierarchy> externalHierarchies, out string defaultHierarchyUniqueName)
		{
			MdxDimension mdxDimension;
			Dictionary<string, MdxHierarchy> dictionary;
			if (variable.Dimension != null && mdxCube.Dimensions.TryGetValue(variable.Dimension, out mdxDimension))
			{
				defaultHierarchyUniqueName = SapBwVariableHierarchyNodeProvider.GetDefaultValueForAssociatedHierarchy(variable.Dimension, mdxCube);
				dictionary = mdxDimension.Hierarchies;
			}
			else
			{
				defaultHierarchyUniqueName = null;
				dictionary = externalHierarchies;
			}
			if (dictionary != null && variableHierarchy != null)
			{
				foreach (KeyValuePair<string, MdxHierarchy> keyValuePair in dictionary)
				{
					if (variableHierarchy.Equals(keyValuePair.Value.MdxIdentifier))
					{
						defaultHierarchyUniqueName = null;
						dictionary = new Dictionary<string, MdxHierarchy> { { keyValuePair.Key, keyValuePair.Value } };
						break;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x0007C0A0 File Offset: 0x0007A2A0
		public static Dictionary<string, MdxHierarchy> GetExternalHierarchies(ISapBwService service, SapBwVariable variable)
		{
			Dictionary<string, MdxHierarchy> dictionary = new Dictionary<string, MdxHierarchy>();
			IDataReaderWithTableSchema dataReaderWithTableSchema;
			if (variable.InfoObject != null && service.TryExtractTable("Parameters/ExternalHierarchies", SapBwVariableHierarchyNodeProvider.GetExternalHierarchiesSelect(variable.InfoObject), out dataReaderWithTableSchema))
			{
				using (dataReaderWithTableSchema)
				{
					while (dataReaderWithTableSchema.Read())
					{
						string @string = dataReaderWithTableSchema.GetString(0);
						string string2 = dataReaderWithTableSchema.GetString(1);
						dictionary.Add(@string, new MdxHierarchy(null, @string, string2, null, MdxHierarchyType.UserDefined, null, true));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x0007C124 File Offset: 0x0007A324
		private static SapBwMetadataAstCreator GetExternalHierarchiesSelect(string infoObject)
		{
			SapBwMetadataAstCreator sapBwMetadataAstCreator = new SapBwMetadataAstCreator(null);
			sapBwMetadataAstCreator.AddSelectColumns(new string[] { "HIEID", "HIENM" });
			sapBwMetadataAstCreator.AddTable("RSHIEDIR");
			sapBwMetadataAstCreator.AddCondition("IOBJNM", TextValue.New(infoObject));
			sapBwMetadataAstCreator.AddCondition("OBJVERS", TextValue.New("A"));
			return sapBwMetadataAstCreator;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x0007C190 File Offset: 0x0007A390
		private static string GetDefaultValueForAssociatedHierarchy(string dimension, SapBwMdxCube mdxCube)
		{
			return (string)(from v in mdxCube.Variables
				where v.Type == SapBwVariableType.Hierarchy && v.Dimension.Equals(dimension)
				select v.DefaultLow).FirstOrDefault<object>();
		}

		// Token: 0x040011FE RID: 4606
		private bool hasMultipleHierarchies;

		// Token: 0x040011FF RID: 4607
		private string singleHierarchyUniqueName;
	}
}
