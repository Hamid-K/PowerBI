using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200046F RID: 1135
	internal sealed class Previous : DataAggregate
	{
		// Token: 0x06003432 RID: 13362 RVA: 0x000E659E File Offset: 0x000E479E
		internal Previous()
		{
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000E65A6 File Offset: 0x000E47A6
		internal Previous(OnDemandProcessingContext odpContext, int startIndex, bool isScopedInEvaluationScope, bool hasNoExplicitScope)
		{
			this.m_odpContext = odpContext;
			this.m_isScopedInEvaluationScope = isScopedInEvaluationScope;
			this.m_hasNoExplicitScope = hasNoExplicitScope;
			this.m_startIndex = startIndex;
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000E65CB File Offset: 0x000E47CB
		internal override void Init()
		{
			if (!this.m_isScopedInEvaluationScope && !this.m_previousEnabled)
			{
				this.m_previousValues = this.m_values;
				this.m_values = new Dictionary<List<object>, object>(Previous.ListOfObjectsEqualityComparer.Instance);
			}
			this.m_previousEnabled = true;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000E6600 File Offset: 0x000E4800
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			if (this.m_isScopedInEvaluationScope)
			{
				if (this.m_previousEnabled || this.m_hasNoExplicitScope)
				{
					this.m_previous = this.m_value;
				}
				this.m_value = expressions[0];
			}
			else
			{
				List<object> list = Previous.ListCloner.CloneList(this.m_odpContext.GroupExpressionValues, this.m_startIndex);
				if (this.m_previousValues != null)
				{
					this.m_previousValues.TryGetValue(list, out this.m_previous);
				}
				this.m_values[list] = expressions[0];
			}
			this.m_previousEnabled = false;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000E6684 File Offset: 0x000E4884
		internal override object Result()
		{
			return this.m_previous;
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000E668C File Offset: 0x000E488C
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Previous;
			}
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000E6690 File Offset: 0x000E4890
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			RunningValueInfo runningValueInfo = (RunningValueInfo)aggregateDef;
			return new Previous(odpContext, runningValueInfo.TotalGroupingExpressionCount, runningValueInfo.IsScopedInEvaluationScope, string.IsNullOrEmpty(runningValueInfo.Scope));
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000E66C4 File Offset: 0x000E48C4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Previous.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.OdpContext)
				{
					if (memberName == MemberName.Value)
					{
						writer.Write(this.m_value);
						continue;
					}
					if (memberName == MemberName.IsScopedInEvaluationScope)
					{
						writer.Write(this.m_isScopedInEvaluationScope);
						continue;
					}
					if (memberName == MemberName.OdpContext)
					{
						int num = scalabilityCache.StoreStaticReference(this.m_odpContext);
						writer.Write(num);
						continue;
					}
				}
				else if (memberName <= MemberName.StartHidden)
				{
					if (memberName == MemberName.Previous)
					{
						writer.Write(this.m_previous);
						continue;
					}
					if (memberName == MemberName.StartHidden)
					{
						writer.Write(this.m_startIndex);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Values)
					{
						writer.WriteVariantListVariantDictionary(this.m_values);
						continue;
					}
					switch (memberName)
					{
					case MemberName.PreviousValues:
						writer.WriteVariantListVariantDictionary(this.m_previousValues);
						continue;
					case MemberName.PreviousEnabled:
						writer.Write(this.m_previousEnabled);
						continue;
					case MemberName.HasNoExplicitScope:
						writer.Write(this.m_hasNoExplicitScope);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000E680C File Offset: 0x000E4A0C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Previous.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.OdpContext)
				{
					if (memberName == MemberName.Value)
					{
						this.m_value = reader.ReadVariant();
						continue;
					}
					if (memberName == MemberName.IsScopedInEvaluationScope)
					{
						this.m_isScopedInEvaluationScope = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.OdpContext)
					{
						int num = reader.ReadInt32();
						this.m_odpContext = (OnDemandProcessingContext)scalabilityCache.FetchStaticReference(num);
						continue;
					}
				}
				else if (memberName <= MemberName.StartHidden)
				{
					if (memberName == MemberName.Previous)
					{
						this.m_previous = reader.ReadVariant();
						continue;
					}
					if (memberName == MemberName.StartHidden)
					{
						this.m_startIndex = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Values)
					{
						this.m_values = reader.ReadVariantListVariantDictionary();
						continue;
					}
					switch (memberName)
					{
					case MemberName.PreviousValues:
						this.m_previousValues = reader.ReadVariantListVariantDictionary();
						continue;
					case MemberName.PreviousEnabled:
						this.m_previousEnabled = reader.ReadBoolean();
						continue;
					case MemberName.HasNoExplicitScope:
						this.m_hasNoExplicitScope = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000E6957 File Offset: 0x000E4B57
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000E6959 File Offset: 0x000E4B59
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Previous;
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000E6960 File Offset: 0x000E4B60
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Previous.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Previous, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.OdpContext, Token.Int32),
					new MemberInfo(MemberName.PreviousValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantListVariantDictionary),
					new MemberInfo(MemberName.Values, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantListVariantDictionary),
					new MemberInfo(MemberName.StartIndex, Token.Int32),
					new MemberInfo(MemberName.IsScopedInEvaluationScope, Token.Boolean),
					new MemberInfo(MemberName.Previous, Token.Object),
					new MemberInfo(MemberName.PreviousEnabled, Token.Boolean),
					new MemberInfo(MemberName.HasNoExplicitScope, Token.Boolean),
					new MemberInfo(MemberName.Value, Token.Object)
				});
			}
			return Previous.m_declaration;
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000E6A2F File Offset: 0x000E4C2F
		public override int Size
		{
			get
			{
				return ItemSizes.ReferenceSize + ItemSizes.SizeOf<List<object>, object>(this.m_previousValues) + ItemSizes.SizeOf<List<object>, object>(this.m_values) + 4 + 1 + ItemSizes.SizeOf(this.m_previous) + 1 + 1 + ItemSizes.SizeOf(this.m_value);
			}
		}

		// Token: 0x040019F0 RID: 6640
		[StaticReference]
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x040019F1 RID: 6641
		private Dictionary<List<object>, object> m_previousValues;

		// Token: 0x040019F2 RID: 6642
		private Dictionary<List<object>, object> m_values;

		// Token: 0x040019F3 RID: 6643
		private int m_startIndex;

		// Token: 0x040019F4 RID: 6644
		private bool m_isScopedInEvaluationScope;

		// Token: 0x040019F5 RID: 6645
		private object m_previous;

		// Token: 0x040019F6 RID: 6646
		private bool m_previousEnabled;

		// Token: 0x040019F7 RID: 6647
		private bool m_hasNoExplicitScope;

		// Token: 0x040019F8 RID: 6648
		private object m_value;

		// Token: 0x040019F9 RID: 6649
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Previous.GetDeclaration();

		// Token: 0x0200096D RID: 2413
		private class ListCloner
		{
			// Token: 0x0600805A RID: 32858 RVA: 0x00210DFB File Offset: 0x0020EFFB
			private ListCloner()
			{
			}

			// Token: 0x0600805B RID: 32859 RVA: 0x00210E04 File Offset: 0x0020F004
			internal static List<object> CloneList(List<object> key, int startIndex)
			{
				if (key == null)
				{
					return null;
				}
				int num = key.Count - startIndex;
				List<object> list = new List<object>(Math.Max(0, num));
				for (int i = 0; i < num; i++)
				{
					list.Add(key[i + startIndex]);
				}
				return list;
			}
		}

		// Token: 0x0200096E RID: 2414
		internal class ListOfObjectsEqualityComparer : IEqualityComparer<List<object>>
		{
			// Token: 0x0600805C RID: 32860 RVA: 0x00210E48 File Offset: 0x0020F048
			private ListOfObjectsEqualityComparer()
			{
			}

			// Token: 0x0600805D RID: 32861 RVA: 0x00210E50 File Offset: 0x0020F050
			public bool Equals(List<object> x, List<object> y)
			{
				if (x == null && y == null)
				{
					return true;
				}
				if (x == null != (y == null) || x.Count != y.Count)
				{
					return false;
				}
				for (int i = x.Count - 1; i >= 0; i--)
				{
					if (!x[i].Equals(y[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600805E RID: 32862 RVA: 0x00210EAC File Offset: 0x0020F0AC
			public int GetHashCode(List<object> obj)
			{
				int count = obj.Count;
				int num = count << 24;
				if (count > 0)
				{
					num ^= obj[count - 1].GetHashCode();
				}
				return num;
			}

			// Token: 0x040040BE RID: 16574
			internal static readonly Previous.ListOfObjectsEqualityComparer Instance = new Previous.ListOfObjectsEqualityComparer();
		}
	}
}
