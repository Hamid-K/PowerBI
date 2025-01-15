using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004FA RID: 1274
	internal class SapBwVariableHierarchyNodeTableProvider : SapBwVariableHierarchyNodeProvider
	{
		// Token: 0x0600298F RID: 10639 RVA: 0x0007C221 File Offset: 0x0007A421
		public SapBwVariableHierarchyNodeTableProvider(ISapBwService service, SapBwMdxCube mdxCube, SapBwVariable variable, bool allowNonAssigned)
			: base(service, mdxCube, variable, allowNonAssigned)
		{
			variable.EnsureInfoObjectDetails(service);
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x0007C238 File Offset: 0x0007A438
		public override bool HasValues
		{
			get
			{
				this.EnsureInitialized();
				SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider sapBwHierarchyNodeProvider = this.valueProviders.FirstOrDefault<SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider>();
				return sapBwHierarchyNodeProvider != null && sapBwHierarchyNodeProvider.HasValues;
			}
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0007C262 File Offset: 0x0007A462
		public override IEnumerable<IValueReference> GetValues()
		{
			this.EnsureInitialized();
			foreach (SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider sapBwHierarchyNodeProvider in this.valueProviders)
			{
				foreach (IValueReference valueReference in sapBwHierarchyNodeProvider.GetValues())
				{
					yield return valueReference;
				}
				IEnumerator<IValueReference> enumerator2 = null;
			}
			List<SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider>.Enumerator enumerator = default(List<SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0007C272 File Offset: 0x0007A472
		protected override void EnsureInitialized()
		{
			if (this.valueProviders == null)
			{
				this.valueProviders = new List<SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider>();
				base.BuildValueProviders();
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0007C290 File Offset: 0x0007A490
		protected override void AddValueProvider(MdxHierarchy defaultHierarchy, Dictionary<string, MdxHierarchy> hierarchyNames)
		{
			List<string> list;
			if (base.Variable.CaptionSource != null)
			{
				(list = new List<string>()).Add(base.Variable.CaptionSource);
			}
			else
			{
				List<string> list2 = new List<string>();
				list2.Add("TXTSH");
				list2.Add("TXTMD");
				list = list2;
				list2.Add("TXTLG");
			}
			List<string> list3 = list;
			if (defaultHierarchy != null)
			{
				this.valueProviders.Add(new SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider(this, this.BuildHierarchyNodesSql(list3, new string[] { defaultHierarchy.UniqueIdentifier }, null), new Dictionary<string, MdxHierarchy> { { defaultHierarchy.UniqueIdentifier, defaultHierarchy } }));
				return;
			}
			this.valueProviders.Add(new SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider(this, this.BuildHierarchyNodesSql(list3, hierarchyNames.Keys.ToArray<string>(), new long?(2500L)), hierarchyNames));
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0007C35E File Offset: 0x0007A55E
		protected override Dictionary<string, MdxHierarchy> GetExternalHierarchies(SapBwVariable variable)
		{
			return SapBwVariableHierarchyNodeProvider.GetExternalHierarchies(base.Service, variable);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0007C36C File Offset: 0x0007A56C
		private SapBwMetadataAstCreator BuildHierarchyNodesSql(IEnumerable<string> textFields, string[] keys, long? top = null)
		{
			SapBwMetadataAstCreator sapBwMetadataAstCreator = new SapBwMetadataAstCreator(top);
			sapBwMetadataAstCreator.AddSelectColumns(new string[] { "HIEID", "NODENAME" });
			foreach (string text in textFields)
			{
				sapBwMetadataAstCreator.AddSelectColumns(new string[] { text });
			}
			sapBwMetadataAstCreator.AddTable("RSTHIERNODE");
			sapBwMetadataAstCreator.AddCondition("HIEID", ListValue.New(keys));
			sapBwMetadataAstCreator.AddCondition("OBJVERS", TextValue.New("A"));
			sapBwMetadataAstCreator.AddCondition("LANGU", TextValue.New(base.Service.Language));
			return sapBwMetadataAstCreator;
		}

		// Token: 0x04001203 RID: 4611
		private List<SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider> valueProviders;

		// Token: 0x020004FB RID: 1275
		private class SapBwHierarchyNodeProvider
		{
			// Token: 0x06002996 RID: 10646 RVA: 0x0007C430 File Offset: 0x0007A630
			public SapBwHierarchyNodeProvider(SapBwVariableHierarchyNodeProvider valueProvider, SapBwMetadataAstCreator astCreator, Dictionary<string, MdxHierarchy> hierarchies)
			{
				this.provider = valueProvider;
				this.astCreator = astCreator;
				this.hierarchies = hierarchies;
			}

			// Token: 0x17000FFC RID: 4092
			// (get) Token: 0x06002997 RID: 10647 RVA: 0x0007C44D File Offset: 0x0007A64D
			public bool HasValues
			{
				get
				{
					this.EnsureBuffered();
					return this.cachedValues.Count > 0;
				}
			}

			// Token: 0x06002998 RID: 10648 RVA: 0x0007C463 File Offset: 0x0007A663
			public IEnumerable<IValueReference> GetValues()
			{
				this.EnsureBuffered();
				return this.cachedValues;
			}

			// Token: 0x06002999 RID: 10649 RVA: 0x0007C474 File Offset: 0x0007A674
			private void EnsureBuffered()
			{
				if (this.cachedValues == null)
				{
					this.cachedValues = new List<IValueReference>();
					IDataReaderWithTableSchema dataReaderWithTableSchema;
					if (this.provider.Service.TryExtractTable("Parameters/HierarchyNodeValues", this.astCreator, out dataReaderWithTableSchema))
					{
						using (dataReaderWithTableSchema)
						{
							while (dataReaderWithTableSchema.Read())
							{
								string text = SapBwVariableHierarchyNodeTableProvider.SapBwHierarchyNodeProvider.ExtractCaption(this.provider.Variable.CaptionSource, dataReaderWithTableSchema);
								string text2 = string.Empty;
								MdxHierarchy mdxHierarchy;
								if (this.hierarchies.TryGetValue(dataReaderWithTableSchema.GetString(0), out mdxHierarchy))
								{
									if (this.provider.HasMultipleHierarchies)
									{
										text = mdxHierarchy.Caption + ": " + text;
									}
									text2 = mdxHierarchy.MdxIdentifier + ".";
								}
								Value value = TextValue.New(text2 + SapBwIdentifier.TrimAndQuotePart(dataReaderWithTableSchema.GetString(1), null));
								value = NavigationTableServices.AddCaption(value, text);
								this.cachedValues.Add(value);
							}
						}
					}
				}
			}

			// Token: 0x0600299A RID: 10650 RVA: 0x0007C588 File Offset: 0x0007A788
			private static string ExtractCaption(string captionSource, IDataReader reader)
			{
				string text;
				if (captionSource == null)
				{
					text = reader.GetString(4);
					if (string.IsNullOrEmpty(text))
					{
						text = reader.GetString(3);
						if (string.IsNullOrEmpty(text))
						{
							text = reader.GetString(2);
							if (string.IsNullOrEmpty(text))
							{
								text = reader.GetString(1);
							}
						}
					}
				}
				else
				{
					text = reader.GetString(2);
					if (string.IsNullOrEmpty(text))
					{
						text = reader.GetString(1);
					}
				}
				return text;
			}

			// Token: 0x04001204 RID: 4612
			private readonly SapBwMetadataAstCreator astCreator;

			// Token: 0x04001205 RID: 4613
			private readonly Dictionary<string, MdxHierarchy> hierarchies;

			// Token: 0x04001206 RID: 4614
			private readonly SapBwVariableHierarchyNodeProvider provider;

			// Token: 0x04001207 RID: 4615
			private List<IValueReference> cachedValues;

			// Token: 0x04001208 RID: 4616
			private const int hierarchyIdOrdinal = 0;

			// Token: 0x04001209 RID: 4617
			private const int nodeNameOrdinal = 1;

			// Token: 0x0400120A RID: 4618
			private const int textOrdinal = 2;

			// Token: 0x0400120B RID: 4619
			private const int shortTextOrdinal = 2;

			// Token: 0x0400120C RID: 4620
			private const int mediumTextOrdinal = 3;

			// Token: 0x0400120D RID: 4621
			private const int longTextOrdinal = 4;
		}
	}
}
