using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000544 RID: 1348
	internal class PersistenceValidator
	{
		// Token: 0x06004997 RID: 18839 RVA: 0x00136E01 File Offset: 0x00135001
		[Conditional("DEBUG")]
		internal static void VerifyReadOrWrite(MemberInfo CurrentMember, PersistMethod persistMethod)
		{
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x00136E04 File Offset: 0x00135004
		[Conditional("DEBUG")]
		internal static void VerifyReadOrWrite(MemberInfo currentMember, PersistMethod persistMethod, Token primitiveType, ObjectType containedType)
		{
			switch (persistMethod)
			{
			case PersistMethod.PrimitiveGenericList:
			case PersistMethod.PrimitiveList:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.PrimitiveList);
				Global.Tracer.Assert(currentMember.Token == primitiveType);
				return;
			case PersistMethod.PrimitiveArray:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.PrimitiveArray);
				Global.Tracer.Assert(currentMember.Token == primitiveType || currentMember.ContainedType == containedType);
				return;
			case PersistMethod.PrimitiveTypedArray:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.PrimitiveTypedArray);
				Global.Tracer.Assert(currentMember.Token == primitiveType);
				return;
			case PersistMethod.GenericListOfReferences:
			case PersistMethod.ListOfReferences:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.RIFObjectList);
				Global.Tracer.Assert(currentMember.Token == Token.Reference);
				return;
			case PersistMethod.GenericListOfGlobalReferences:
			case PersistMethod.ListOfGlobalReferences:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.RIFObjectList);
				Global.Tracer.Assert(currentMember.Token == Token.GlobalReference);
				return;
			case PersistMethod.SerializableArray:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.SerializableArray);
				Global.Tracer.Assert(currentMember.Token == Token.Serializable);
				return;
			}
			Global.Tracer.Assert(false, string.Format("ReportIntermediateFormat.Persistence does not support {0}.{1}.", persistMethod.GetType(), persistMethod));
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x00136F68 File Offset: 0x00135168
		[Conditional("DEBUG")]
		internal static void VerifyDeclaredType(MemberInfo currentMember, ObjectType persistedType, Dictionary<ObjectType, Declaration> declarations, bool verify)
		{
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x00136F6C File Offset: 0x0013516C
		[Conditional("DEBUG")]
		internal static void VerifyDeclaredType(MemberInfo currentMember, ObjectType persistedType, Dictionary<ObjectType, Declaration> declarations)
		{
			if (declarations == null)
			{
				return;
			}
			if (currentMember.ContainedType == ObjectType.RIFObjectArray || currentMember.ContainedType == ObjectType.RIFObjectList)
			{
				return;
			}
			if (!PersistenceValidator.VerifyDeclaredType(currentMember.ObjectType, persistedType, declarations) && !PersistenceValidator.VerifyDeclaredType(currentMember.ContainedType, persistedType, declarations) && !PersistenceValidator.CheckSpecialCase(currentMember, persistedType))
			{
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x00136FC4 File Offset: 0x001351C4
		private static bool VerifyDeclaredType(ObjectType declaredType, ObjectType persistedType, Dictionary<ObjectType, Declaration> declarations)
		{
			if (persistedType == declaredType || declaredType == ObjectType.RIFObject)
			{
				return true;
			}
			Declaration declaration;
			if (declarations.TryGetValue(persistedType, out declaration))
			{
				while (declaration.BaseObjectType != ObjectType.None)
				{
					if (declaration.BaseObjectType == declaredType)
					{
						return true;
					}
					declaration = declarations[declaration.BaseObjectType];
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x00137010 File Offset: 0x00135210
		internal static bool CheckSpecialCase(MemberInfo currentMember, ObjectType persistedType)
		{
			for (int i = 0; i < 2; i++)
			{
				ObjectType objectType;
				if (i == 0)
				{
					objectType = currentMember.ObjectType;
				}
				else
				{
					objectType = currentMember.ContainedType;
				}
				if (objectType <= ObjectType.ISortDataHolder)
				{
					if (objectType <= ObjectType.ISortFilterScope)
					{
						if (objectType != ObjectType.IScalableDictionaryEntry)
						{
							if (objectType == ObjectType.ISortFilterScope)
							{
								if (persistedType <= ObjectType.CustomReportItem)
								{
									if (persistedType <= ObjectType.Grouping)
									{
										if (persistedType != ObjectType.DataRegion && persistedType != ObjectType.Grouping)
										{
											goto IL_02FB;
										}
									}
									else if (persistedType != ObjectType.DataSet && persistedType != ObjectType.CustomReportItem)
									{
										goto IL_02FB;
									}
								}
								else if (persistedType <= ObjectType.Chart)
								{
									if (persistedType != ObjectType.Tablix && persistedType != ObjectType.Chart)
									{
										goto IL_02FB;
									}
								}
								else if (persistedType != ObjectType.GaugePanel && persistedType != ObjectType.MapDataRegion)
								{
									goto IL_02FB;
								}
								return true;
							}
						}
						else if (persistedType == ObjectType.ScalableDictionaryNodeReference || persistedType - ObjectType.ScalableDictionaryValues <= 1)
						{
							return true;
						}
					}
					else if (objectType != ObjectType.IInScopeEventSource)
					{
						if (objectType == ObjectType.IVisibilityOwner)
						{
							if (persistedType <= ObjectType.Tablix)
							{
								if (persistedType <= ObjectType.SubReport)
								{
									if (persistedType != ObjectType.ReportItem && persistedType - ObjectType.Line > 4)
									{
										goto IL_02FB;
									}
								}
								else if (persistedType != ObjectType.CustomReportItem && persistedType != ObjectType.Tablix)
								{
									goto IL_02FB;
								}
							}
							else if (persistedType <= ObjectType.Chart)
							{
								if (persistedType != ObjectType.TablixMember && persistedType != ObjectType.Chart)
								{
									goto IL_02FB;
								}
							}
							else if (persistedType != ObjectType.GaugePanel && persistedType != ObjectType.Map)
							{
								goto IL_02FB;
							}
							return true;
						}
						if (objectType == ObjectType.ISortDataHolder)
						{
							if (persistedType - ObjectType.RuntimeTablixGroupLeafObj <= 1 || persistedType == ObjectType.RuntimeSortDataHolder)
							{
								return true;
							}
						}
					}
					else if (persistedType == ObjectType.TextBox)
					{
						return true;
					}
				}
				else if (objectType <= ObjectType.DataAggregate)
				{
					if (objectType == ObjectType.IHierarchyObj)
					{
						if (persistedType <= ObjectType.RuntimeOnDemandDataSetObj)
						{
							if (persistedType <= ObjectType.SortExpressionScopeInstanceHolder)
							{
								if (persistedType != ObjectType.RuntimeSortHierarchyObj && persistedType - ObjectType.SortFilterExpressionScopeObj > 1)
								{
									goto IL_02FB;
								}
							}
							else
							{
								switch (persistedType)
								{
								case ObjectType.RuntimeHierarchyObj:
								case ObjectType.RuntimeDataTablixGroupRootObj:
								case ObjectType.RuntimeDataTablixObj:
								case ObjectType.RuntimeTablixObj:
								case ObjectType.RuntimeChartObj:
								case ObjectType.RuntimeCriObj:
								case ObjectType.RuntimeTablixGroupLeafObj:
								case ObjectType.RuntimeChartCriGroupLeafObj:
									break;
								case ObjectType.RuntimeGroupingObj:
								case ObjectType.VariantRifObjectDictionary:
								case ObjectType.VariantListOfRifObjectDictionary:
								case ObjectType.AggregatesImpl:
								case ObjectType.RuntimeDataTablixMemberObj:
								case ObjectType.RuntimeTablixMemberObj:
									goto IL_02FB;
								default:
									if (persistedType != ObjectType.RuntimeOnDemandDataSetObj)
									{
										goto IL_02FB;
									}
									break;
								}
							}
						}
						else if (persistedType <= ObjectType.RuntimeDataTablixGroupLeafObj)
						{
							if (persistedType - ObjectType.IHierarchyObj > 1)
							{
								switch (persistedType)
								{
								case ObjectType.RuntimeGroupLeafObj:
								case ObjectType.RuntimeGroupObj:
								case ObjectType.RuntimeDetailObj:
								case ObjectType.RuntimeGroupRootObj:
								case ObjectType.RuntimeChartCriObj:
								case ObjectType.RuntimeDataTablixGroupLeafObj:
									break;
								case ObjectType.IErrorContext:
								case ObjectType.RuntimeMemberObj:
									goto IL_02FB;
								default:
									goto IL_02FB;
								}
							}
						}
						else if (persistedType != ObjectType.RuntimeGaugePanelObj && persistedType != ObjectType.RuntimeMapDataRegionObj && persistedType != ObjectType.RuntimeDataRowSortHierarchyObj)
						{
							goto IL_02FB;
						}
						return true;
					}
					if (objectType == ObjectType.DataAggregate)
					{
						switch (persistedType)
						{
						case ObjectType.Aggregate:
						case ObjectType.First:
						case ObjectType.Last:
						case ObjectType.Sum:
						case ObjectType.Avg:
						case ObjectType.Max:
						case ObjectType.Min:
						case ObjectType.Count:
						case ObjectType.CountDistinct:
						case ObjectType.CountRows:
						case ObjectType.Var:
						case ObjectType.StDev:
						case ObjectType.VarP:
						case ObjectType.StDevP:
						case ObjectType.Previous:
							break;
						case ObjectType.VariantVariantHashtable:
						case ObjectType.VarBase:
							goto IL_02FB;
						default:
							if (persistedType != ObjectType.Union)
							{
								goto IL_02FB;
							}
							break;
						}
						return true;
					}
				}
				else if (objectType != ObjectType.ISortDataHolderReference)
				{
					if (objectType != ObjectType.IRowItemStruct)
					{
						if (objectType == ObjectType.TablixItemStruct)
						{
							if (persistedType - ObjectType.TablixStruct <= 1)
							{
								return true;
							}
						}
					}
					else if (persistedType - ObjectType.RowItemStruct <= 3)
					{
						return true;
					}
				}
				else if (persistedType - ObjectType.RuntimeTablixGroupLeafObjReference <= 1)
				{
					return true;
				}
				IL_02FB:;
			}
			return false;
		}
	}
}
