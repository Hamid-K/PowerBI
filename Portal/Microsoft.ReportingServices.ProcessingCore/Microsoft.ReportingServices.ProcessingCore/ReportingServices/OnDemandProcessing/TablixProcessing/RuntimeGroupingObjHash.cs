using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C6 RID: 2246
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeGroupingObjHash : RuntimeGroupingObj
	{
		// Token: 0x06007B06 RID: 31494 RVA: 0x001FA783 File Offset: 0x001F8983
		internal RuntimeGroupingObjHash()
		{
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x001FA78C File Offset: 0x001F898C
		internal RuntimeGroupingObjHash(RuntimeHierarchyObj owner, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(owner, objectType)
		{
			OnDemandProcessingContext odpContext = owner.OdpContext;
			this.m_hashtable = new ScalableDictionary<object, IReference<RuntimeHierarchyObj>>(owner.Depth + 1, odpContext.TablixProcessingScalabilityCache, 101, 27, odpContext.ProcessingComparer);
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x001FA7CB File Offset: 0x001F89CB
		internal override void Cleanup()
		{
			if (this.m_hashtable != null)
			{
				this.m_hashtable.Dispose();
				this.m_hashtable = null;
			}
			if (this.m_parentInfo != null)
			{
				this.m_parentInfo.Dispose();
				this.m_parentInfo = null;
			}
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x001FA804 File Offset: 0x001F8A04
		internal override void NextRow(object keyValue, bool hasParent, object parentKey)
		{
			IReference<RuntimeHierarchyObj> reference = null;
			try
			{
				this.m_hashtable.TryGetValue(keyValue, out reference);
			}
			catch (ReportProcessingException_SpatialTypeComparisonError reportProcessingException_SpatialTypeComparisonError)
			{
				throw new ReportProcessingException(this.m_owner.RegisterSpatialTypeComparisonError(reportProcessingException_SpatialTypeComparisonError.Type));
			}
			catch (ReportProcessingException_ComparisonError reportProcessingException_ComparisonError)
			{
				throw new ReportProcessingException(this.m_owner.RegisterComparisonError("GroupExpression", reportProcessingException_ComparisonError));
			}
			if (reference != null)
			{
				using (reference.PinValue())
				{
					reference.Value().NextRow();
					return;
				}
			}
			RuntimeHierarchyObj runtimeHierarchyObj = new RuntimeHierarchyObj(this.m_owner, this.m_objectType, ((IScope)this.m_owner).Depth + 1);
			reference = (IReference<RuntimeHierarchyObj>)runtimeHierarchyObj.SelfReference;
			try
			{
				this.m_hashtable.Add(keyValue, reference);
				runtimeHierarchyObj = reference.Value();
				runtimeHierarchyObj.NextRow();
				if (hasParent)
				{
					IReference<RuntimeHierarchyObj> reference2 = null;
					IReference<RuntimeGroupLeafObj> reference3 = null;
					try
					{
						this.m_hashtable.TryGetValue(parentKey, out reference2);
					}
					catch (ReportProcessingException_SpatialTypeComparisonError reportProcessingException_SpatialTypeComparisonError2)
					{
						throw new ReportProcessingException(this.m_owner.RegisterSpatialTypeComparisonError(reportProcessingException_SpatialTypeComparisonError2.Type));
					}
					catch (ReportProcessingException_ComparisonError reportProcessingException_ComparisonError2)
					{
						throw new ReportProcessingException(this.m_owner.RegisterComparisonError("Parent", reportProcessingException_ComparisonError2));
					}
					if (reference2 != null)
					{
						RuntimeHierarchyObj runtimeHierarchyObj2 = reference2.Value();
						Global.Tracer.Assert(runtimeHierarchyObj2.HierarchyObjs != null, "(null != parentHierarchyObj.HierarchyObjs)");
						reference3 = (RuntimeGroupLeafObjReference)runtimeHierarchyObj2.HierarchyObjs[0];
					}
					Global.Tracer.Assert(runtimeHierarchyObj.HierarchyObjs != null, "(null != hierarchyObj.HierarchyObjs)");
					RuntimeGroupLeafObjReference runtimeGroupLeafObjReference = (RuntimeGroupLeafObjReference)runtimeHierarchyObj.HierarchyObjs[0];
					bool flag = true;
					if (reference3 == runtimeGroupLeafObjReference)
					{
						reference3 = null;
						flag = false;
					}
					this.ProcessChildren(keyValue, reference3, runtimeGroupLeafObjReference);
					this.ProcessParent(parentKey, reference3, runtimeGroupLeafObjReference, flag);
				}
			}
			finally
			{
				reference.UnPinValue();
			}
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x001FA9F8 File Offset: 0x001F8BF8
		internal override void Traverse(ProcessingStages operation, bool ascending, ITraversalContext traversalContext)
		{
			RuntimeGroupRootObj runtimeGroupRootObj = this.m_owner as RuntimeGroupRootObj;
			Global.Tracer.Assert(runtimeGroupRootObj != null, "(null != groupRootOwner)");
			runtimeGroupRootObj.TraverseLinkedGroupLeaves(operation, ascending, traversalContext);
		}

		// Token: 0x06007B0B RID: 31499 RVA: 0x001FAA30 File Offset: 0x001F8C30
		internal override void CopyDomainScopeGroupInstances(RuntimeGroupRootObj destination)
		{
			DomainScopeContext domainScopeContext = this.m_owner.OdpContext.DomainScopeContext;
			domainScopeContext.CurrentDomainScope = new DomainScopeContext.DomainScopeInfo();
			domainScopeContext.CurrentDomainScope.InitializeKeys((this.m_owner as RuntimeGroupRootObj).GroupExpressions.Count);
			this.CopyDomainScopeGroupInstance(destination, this.m_hashtable);
			domainScopeContext.CurrentDomainScope = null;
		}

		// Token: 0x06007B0C RID: 31500 RVA: 0x001FAA8C File Offset: 0x001F8C8C
		private void CopyDomainScopeGroupInstance(RuntimeGroupRootObj destination, ScalableDictionary<object, IReference<RuntimeHierarchyObj>> runtimeHierarchyObjRefs)
		{
			DomainScopeContext.DomainScopeInfo currentDomainScope = this.m_owner.OdpContext.DomainScopeContext.CurrentDomainScope;
			foreach (object obj in runtimeHierarchyObjRefs.Keys)
			{
				currentDomainScope.AddKey(obj);
				IReference<RuntimeHierarchyObj> reference = runtimeHierarchyObjRefs[obj];
				using (reference.PinValue())
				{
					RuntimeHierarchyObj runtimeHierarchyObj = reference.Value();
					if (runtimeHierarchyObj.HierarchyObjs == null)
					{
						RuntimeGroupingObjHash runtimeGroupingObjHash = (RuntimeGroupingObjHash)runtimeHierarchyObj.Grouping;
						this.CopyDomainScopeGroupInstance(destination, runtimeGroupingObjHash.m_hashtable);
					}
					else
					{
						Global.Tracer.Assert(runtimeHierarchyObj.HierarchyObjs.Count == 1, "hierarchyObject.HierarchyObjs.Count == 1");
						IReference<RuntimeHierarchyObj> reference2 = runtimeHierarchyObj.HierarchyObjs[0];
						using (reference2.PinValue())
						{
							RuntimeDataTablixGroupLeafObj runtimeDataTablixGroupLeafObj = (RuntimeDataTablixGroupLeafObj)reference2.Value();
							currentDomainScope.CurrentRow = runtimeDataTablixGroupLeafObj.FirstRow;
							destination.NextRow();
						}
					}
				}
				currentDomainScope.RemoveKey();
			}
		}

		// Token: 0x06007B0D RID: 31501 RVA: 0x001FABC8 File Offset: 0x001F8DC8
		private void ProcessParent(object parentKey, IReference<RuntimeGroupLeafObj> parentObj, RuntimeGroupLeafObjReference childObj, bool addToWaitList)
		{
			if (parentObj != null)
			{
				using (parentObj.PinValue())
				{
					parentObj.Value().AddChild(childObj);
					return;
				}
			}
			(this.m_owner as RuntimeGroupRootObj).AddChild(childObj);
			if (addToWaitList)
			{
				ChildLeafInfo childLeafInfo = null;
				IDisposable disposable2 = null;
				try
				{
					if (this.m_parentInfo == null)
					{
						this.m_parentInfo = this.CreateParentInfo();
					}
					else
					{
						this.m_parentInfo.TryGetAndPin(parentKey, out childLeafInfo, out disposable2);
					}
					if (childLeafInfo == null)
					{
						childLeafInfo = new ChildLeafInfo();
						disposable2 = this.m_parentInfo.AddAndPin(parentKey, childLeafInfo);
					}
					childLeafInfo.Add(childObj);
				}
				finally
				{
					if (disposable2 != null)
					{
						disposable2.Dispose();
					}
				}
			}
		}

		// Token: 0x06007B0E RID: 31502 RVA: 0x001FAC80 File Offset: 0x001F8E80
		private ScalableDictionary<object, ChildLeafInfo> CreateParentInfo()
		{
			OnDemandProcessingContext odpContext = this.m_owner.OdpContext;
			return new ScalableDictionary<object, ChildLeafInfo>(this.m_owner.Depth, odpContext.TablixProcessingScalabilityCache, 101, 27);
		}

		// Token: 0x06007B0F RID: 31503 RVA: 0x001FACB4 File Offset: 0x001F8EB4
		private void ProcessChildren(object thisKey, IReference<RuntimeGroupLeafObj> parentObj, IReference<RuntimeGroupLeafObj> thisObj)
		{
			ChildLeafInfo childLeafInfo = null;
			if (this.m_parentInfo != null)
			{
				this.m_parentInfo.TryGetValue(thisKey, out childLeafInfo);
			}
			if (childLeafInfo != null)
			{
				for (int i = 0; i < childLeafInfo.Count; i++)
				{
					RuntimeGroupLeafObjReference runtimeGroupLeafObjReference = childLeafInfo[i];
					using (runtimeGroupLeafObjReference.PinValue())
					{
						RuntimeGroupLeafObj runtimeGroupLeafObj = runtimeGroupLeafObjReference.Value();
						bool flag = false;
						IReference<RuntimeGroupObj> reference = parentObj as IReference<RuntimeGroupObj>;
						while (reference != null && !flag)
						{
							RuntimeGroupLeafObj runtimeGroupLeafObj2 = reference.Value() as RuntimeGroupLeafObj;
							if (runtimeGroupLeafObj2 == runtimeGroupLeafObj)
							{
								flag = true;
							}
							if (runtimeGroupLeafObj2 == null)
							{
								reference = null;
							}
							else
							{
								reference = runtimeGroupLeafObj2.Parent;
							}
						}
						if (!flag)
						{
							runtimeGroupLeafObj.RemoveFromParent((RuntimeGroupRootObjReference)this.m_owner.SelfReference);
							using (thisObj.PinValue())
							{
								thisObj.Value().AddChild(runtimeGroupLeafObjReference);
							}
						}
					}
				}
				this.m_parentInfo.Remove(thisKey);
			}
		}

		// Token: 0x06007B10 RID: 31504 RVA: 0x001FADC0 File Offset: 0x001F8FC0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGroupingObjHash.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Hashtable)
				{
					if (memberName != MemberName.ParentInfo)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_parentInfo);
					}
				}
				else
				{
					writer.Write(this.m_hashtable);
				}
			}
		}

		// Token: 0x06007B11 RID: 31505 RVA: 0x001FAE34 File Offset: 0x001F9034
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGroupingObjHash.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Hashtable)
				{
					if (memberName != MemberName.ParentInfo)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_parentInfo = reader.ReadRIFObject<ScalableDictionary<object, ChildLeafInfo>>();
					}
				}
				else
				{
					this.m_hashtable = reader.ReadRIFObject<ScalableDictionary<object, IReference<RuntimeHierarchyObj>>>();
				}
			}
		}

		// Token: 0x06007B12 RID: 31506 RVA: 0x001FAEA7 File Offset: 0x001F90A7
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B13 RID: 31507 RVA: 0x001FAEA9 File Offset: 0x001F90A9
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjHash;
		}

		// Token: 0x06007B14 RID: 31508 RVA: 0x001FAEB0 File Offset: 0x001F90B0
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupingObjHash.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjHash, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Hashtable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionary),
					new MemberInfo(MemberName.ParentInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionary)
				});
			}
			return RuntimeGroupingObjHash.m_declaration;
		}

		// Token: 0x17002874 RID: 10356
		// (get) Token: 0x06007B15 RID: 31509 RVA: 0x001FAF01 File Offset: 0x001F9101
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_hashtable) + ItemSizes.SizeOf(this.m_parentInfo);
			}
		}

		// Token: 0x04003D63 RID: 15715
		private ScalableDictionary<object, IReference<RuntimeHierarchyObj>> m_hashtable;

		// Token: 0x04003D64 RID: 15716
		private ScalableDictionary<object, ChildLeafInfo> m_parentInfo;

		// Token: 0x04003D65 RID: 15717
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupingObjHash.GetDeclaration();
	}
}
