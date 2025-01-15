using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B6 RID: 2230
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeCells : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007993 RID: 31123 RVA: 0x001F4A78 File Offset: 0x001F2C78
		internal RuntimeCells()
		{
		}

		// Token: 0x06007994 RID: 31124 RVA: 0x001F4A8E File Offset: 0x001F2C8E
		internal RuntimeCells(int priority, IScalabilityCache cache)
		{
			this.m_collection = new ScalableList<IStorable>(priority, cache, 200, 10);
		}

		// Token: 0x06007995 RID: 31125 RVA: 0x001F4AB8 File Offset: 0x001F2CB8
		internal void AddCell(int key, RuntimeCell cell)
		{
			this.InternalAdd(key, cell);
		}

		// Token: 0x06007996 RID: 31126 RVA: 0x001F4AC2 File Offset: 0x001F2CC2
		internal void AddCell(int key, IReference<RuntimeCell> cellRef)
		{
			this.InternalAdd(key, cellRef);
		}

		// Token: 0x06007997 RID: 31127 RVA: 0x001F4ACC File Offset: 0x001F2CCC
		private void InternalAdd(int key, IStorable cell)
		{
			if (this.Count == 0)
			{
				this.m_firstCellKey = key;
			}
			else
			{
				IDisposable disposable;
				this.GetAndPinCell(this.m_lastCellKey, out disposable).NextCell = key;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			this.m_lastCellKey = key;
			this.m_collection.SetValueWithExtension(key, cell);
		}

		// Token: 0x1700282F RID: 10287
		// (get) Token: 0x06007998 RID: 31128 RVA: 0x001F4B1B File Offset: 0x001F2D1B
		public int Count
		{
			get
			{
				return this.m_collection.Count;
			}
		}

		// Token: 0x06007999 RID: 31129 RVA: 0x001F4B28 File Offset: 0x001F2D28
		internal RuntimeCell GetCell(int key, out RuntimeCellReference cellRef)
		{
			RuntimeCell runtimeCell = null;
			cellRef = null;
			if (key < this.m_collection.Count)
			{
				IStorable storable = this.m_collection[key];
				if (storable != null)
				{
					if (this.IsCellReference(storable))
					{
						cellRef = (RuntimeCellReference)storable;
						runtimeCell = cellRef.Value();
					}
					else
					{
						runtimeCell = (RuntimeCell)storable;
					}
				}
			}
			return runtimeCell;
		}

		// Token: 0x0600799A RID: 31130 RVA: 0x001F4B7C File Offset: 0x001F2D7C
		internal RuntimeCell GetAndPinCell(int key, out IDisposable cleanupRef)
		{
			cleanupRef = null;
			IStorable storable;
			bool flag;
			if (key < this.m_collection.Count)
			{
				cleanupRef = this.m_collection.GetAndPin(key, out storable);
				flag = storable != null;
			}
			else
			{
				storable = null;
				flag = false;
			}
			if (!flag)
			{
				return null;
			}
			if (this.IsCellReference(storable))
			{
				if (cleanupRef != null)
				{
					cleanupRef.Dispose();
				}
				IReference<RuntimeCell> reference = (IReference<RuntimeCell>)storable;
				reference.PinValue();
				cleanupRef = (IDisposable)reference;
				return reference.Value();
			}
			return (RuntimeCell)storable;
		}

		// Token: 0x0600799B RID: 31131 RVA: 0x001F4BF4 File Offset: 0x001F2DF4
		private bool IsCellReference(IStorable cellOrReference)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = cellOrReference.GetObjectType();
			if (objectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCell)
			{
				if (objectType - Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixCell <= 1)
				{
					return false;
				}
				if (objectType - Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixCellReference > 1)
				{
					if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCell)
					{
						goto IL_0043;
					}
					return false;
				}
			}
			else if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellReference)
			{
				if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeIntersection)
				{
					return false;
				}
				if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeIntersectionReference)
				{
					goto IL_0043;
				}
			}
			return true;
			IL_0043:
			Global.Tracer.Assert(false, "Unexpected type in RuntimeCells collection");
			throw new InvalidOperationException();
		}

		// Token: 0x0600799C RID: 31132 RVA: 0x001F4C5C File Offset: 0x001F2E5C
		internal RuntimeCell GetOrCreateCell(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, IReference<RuntimeDataTablixGroupLeafObj> ownerRef, IReference<RuntimeDataTablixGroupRootObj> currOuterGroupRootRef, out IDisposable cleanupRef)
		{
			RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = currOuterGroupRootRef.Value();
			int num = dataRegionDef.OuterGroupingIndexes[runtimeDataTablixGroupRootObj.HierarchyDef.HierarchyDynamicIndex];
			return this.GetOrCreateCellByIndex(num, dataRegionDef, ownerRef, runtimeDataTablixGroupRootObj, out cleanupRef);
		}

		// Token: 0x0600799D RID: 31133 RVA: 0x001F4C90 File Offset: 0x001F2E90
		internal RuntimeCell GetOrCreateCell(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, IReference<RuntimeDataTablixGroupLeafObj> ownerRef, IReference<RuntimeDataTablixGroupRootObj> currOuterGroupRootRef, int groupLeafIndex, out IDisposable cleanupRef)
		{
			RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = currOuterGroupRootRef.Value();
			return this.GetOrCreateCellByIndex(groupLeafIndex, dataRegionDef, ownerRef, runtimeDataTablixGroupRootObj, out cleanupRef);
		}

		// Token: 0x0600799E RID: 31134 RVA: 0x001F4CB4 File Offset: 0x001F2EB4
		private RuntimeCell GetOrCreateCellByIndex(int groupLeafIndex, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, IReference<RuntimeDataTablixGroupLeafObj> ownerRef, RuntimeDataTablixGroupRootObj currOuterGroupRoot, out IDisposable cleanupRef)
		{
			RuntimeCell runtimeCell = this.GetAndPinCell(groupLeafIndex, out cleanupRef);
			if (runtimeCell == null)
			{
				using (ownerRef.PinValue())
				{
					RuntimeDataTablixGroupLeafObj runtimeDataTablixGroupLeafObj = ownerRef.Value();
					if (!RuntimeCell.HasOnlySimpleGroupTreeCells(currOuterGroupRoot.HierarchyDef, runtimeDataTablixGroupLeafObj.MemberDef, dataRegionDef))
					{
						runtimeDataTablixGroupLeafObj.CreateCell(this, groupLeafIndex, currOuterGroupRoot.HierarchyDef, runtimeDataTablixGroupLeafObj.MemberDef, dataRegionDef);
					}
				}
				runtimeCell = this.GetAndPinCell(groupLeafIndex, out cleanupRef);
			}
			return runtimeCell;
		}

		// Token: 0x0600799F RID: 31135 RVA: 0x001F4D30 File Offset: 0x001F2F30
		internal void SortAndFilter(AggregateUpdateContext aggContext)
		{
			this.Traverse(ProcessingStages.SortAndFilter, aggContext);
		}

		// Token: 0x060079A0 RID: 31136 RVA: 0x001F4D3C File Offset: 0x001F2F3C
		private void Traverse(ProcessingStages operation, AggregateUpdateContext context)
		{
			if (this.Count == 0)
			{
				return;
			}
			int num = this.m_firstCellKey;
			int num2;
			do
			{
				num2 = num;
				IDisposable disposable;
				RuntimeCell andPinCell = this.GetAndPinCell(num2, out disposable);
				if (operation != ProcessingStages.SortAndFilter)
				{
					if (operation != ProcessingStages.UpdateAggregates)
					{
						Global.Tracer.Assert(false, "Unknown operation in Traverse");
					}
					else
					{
						andPinCell.UpdateAggregates(context);
					}
				}
				else
				{
					andPinCell.SortAndFilter(context);
				}
				num = andPinCell.NextCell;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			while (num2 != this.m_lastCellKey);
		}

		// Token: 0x060079A1 RID: 31137 RVA: 0x001F4DAA File Offset: 0x001F2FAA
		internal void UpdateAggregates(AggregateUpdateContext aggContext)
		{
			this.Traverse(ProcessingStages.UpdateAggregates, aggContext);
		}

		// Token: 0x060079A2 RID: 31138 RVA: 0x001F4DB4 File Offset: 0x001F2FB4
		internal void CalculateRunningValues(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, IReference<RuntimeDataTablixGroupLeafObj> owner, AggregateUpdateContext aggContext)
		{
			IDisposable disposable;
			RuntimeCell orCreateCell = this.GetOrCreateCell(dataRegionDef, owner, dataRegionDef.CurrentOuterGroupRoot, out disposable);
			if (orCreateCell != null)
			{
				orCreateCell.CalculateRunningValues(groupCol, lastGroup, aggContext);
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060079A3 RID: 31139 RVA: 0x001F4DEC File Offset: 0x001F2FEC
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeCells.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.FirstCell)
				{
					if (memberName != MemberName.LastCell)
					{
						if (memberName != MemberName.Collection)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_collection);
						}
					}
					else
					{
						writer.Write(this.m_lastCellKey);
					}
				}
				else
				{
					writer.Write(this.m_firstCellKey);
				}
			}
		}

		// Token: 0x060079A4 RID: 31140 RVA: 0x001F4E70 File Offset: 0x001F3070
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeCells.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.FirstCell)
				{
					if (memberName != MemberName.LastCell)
					{
						if (memberName != MemberName.Collection)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_collection = reader.ReadRIFObject<ScalableList<IStorable>>();
						}
					}
					else
					{
						this.m_lastCellKey = reader.ReadInt32();
					}
				}
				else
				{
					this.m_firstCellKey = reader.ReadInt32();
				}
			}
		}

		// Token: 0x060079A5 RID: 31141 RVA: 0x001F4EF3 File Offset: 0x001F30F3
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x060079A6 RID: 31142 RVA: 0x001F4EF5 File Offset: 0x001F30F5
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCells;
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x001F4EFC File Offset: 0x001F30FC
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeCells.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCells, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.FirstCell, Token.Int32),
					new MemberInfo(MemberName.LastCell, Token.Int32),
					new MemberInfo(MemberName.Collection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList)
				});
			}
			return RuntimeCells.m_declaration;
		}

		// Token: 0x17002830 RID: 10288
		// (get) Token: 0x060079A8 RID: 31144 RVA: 0x001F4F61 File Offset: 0x001F3161
		public int Size
		{
			get
			{
				return 8 + ItemSizes.SizeOf<IStorable>(this.m_collection);
			}
		}

		// Token: 0x04003D06 RID: 15622
		private int m_firstCellKey = -1;

		// Token: 0x04003D07 RID: 15623
		private int m_lastCellKey = -1;

		// Token: 0x04003D08 RID: 15624
		private ScalableList<IStorable> m_collection;

		// Token: 0x04003D09 RID: 15625
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeCells.GetDeclaration();
	}
}
