using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200018D RID: 397
	internal sealed class SerializationActivityContext
	{
		// Token: 0x06001852 RID: 6226 RVA: 0x000A38B0 File Offset: 0x000A1AB0
		public SerializationActivityContext(MetadataSerializationMode serializationMode, CompatibilityMode compatibilityMode, int dbCompatibilityLevel, bool partitionsMergedWithTable = false, bool raiseErrorOnUnresolvedLinks = false)
		{
			if (partitionsMergedWithTable && serializationMode != MetadataSerializationMode.Json)
			{
				throw TomInternalException.Create("The 'partitionsMergedWithTable' option is only valid for JSON serialization; this option is not supported in {0} mode!", new object[] { serializationMode });
			}
			this.SerializationMode = serializationMode;
			this.CompatibilityMode = compatibilityMode;
			this.DbCompatibilityLevel = dbCompatibilityLevel;
			this.PartitionsMergedWithTable = partitionsMergedWithTable;
			this.RaiseErrorOnUnresolvedLinks = raiseErrorOnUnresolvedLinks;
			this.ActivityInfo = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x000A391A File Offset: 0x000A1B1A
		public MetadataSerializationMode SerializationMode { get; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x000A3922 File Offset: 0x000A1B22
		public CompatibilityMode CompatibilityMode { get; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x000A392A File Offset: 0x000A1B2A
		public int DbCompatibilityLevel { get; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x000A3932 File Offset: 0x000A1B32
		public bool PartitionsMergedWithTable { get; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x000A393A File Offset: 0x000A1B3A
		public bool RaiseErrorOnUnresolvedLinks { get; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x000A3942 File Offset: 0x000A1B42
		public IDictionary<string, object> ActivityInfo { get; }

		// Token: 0x06001859 RID: 6233 RVA: 0x000A394A File Offset: 0x000A1B4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TValue GetActivityInfo<TValue>(string key)
		{
			return (TValue)((object)this.ActivityInfo[key]);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000A3960 File Offset: 0x000A1B60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryGetActivityInfo<TValue>(string key, out TValue value)
		{
			object obj;
			if (!this.ActivityInfo.TryGetValue(key, out obj))
			{
				value = default(TValue);
				return false;
			}
			value = (TValue)((object)obj);
			return true;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000A3994 File Offset: 0x000A1B94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryExtractActivityInfo<TValue>(string key, out TValue value)
		{
			object obj;
			if (!this.ActivityInfo.TryGetValue(key, out obj))
			{
				value = default(TValue);
				return false;
			}
			value = (TValue)((object)obj);
			return this.ActivityInfo.Remove(key);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x000A39D2 File Offset: 0x000A1BD2
		public void RegistrerObjectForMasterReferenceCrossLinkReconstruction(MetadataObject @object)
		{
			Utils.Verify(this.SerializationMode != MetadataSerializationMode.Xmla);
			if (this.objectsRegisteredForCrossLinkReconstruction == null)
			{
				this.objectsRegisteredForCrossLinkReconstruction = new List<MetadataObject>();
			}
			this.objectsRegisteredForCrossLinkReconstruction.Add(@object);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000A3A04 File Offset: 0x000A1C04
		public bool TryReconstructMasterReferenceCrossLinkForRegistreredObjects(bool keepObjectsWithUnresolvedReference)
		{
			Utils.Verify(this.SerializationMode != MetadataSerializationMode.Xmla);
			if (this.objectsRegisteredForCrossLinkReconstruction == null)
			{
				return true;
			}
			int i = 0;
			while (i < this.objectsRegisteredForCrossLinkReconstruction.Count)
			{
				MetadataObject metadataObject = this.objectsRegisteredForCrossLinkReconstruction[i];
				ObjectType objectType = metadataObject.ObjectType;
				bool flag;
				switch (objectType)
				{
				case ObjectType.PerspectiveTable:
					flag = true;
					break;
				case ObjectType.PerspectiveColumn:
				case ObjectType.PerspectiveHierarchy:
				case ObjectType.PerspectiveMeasure:
				case ObjectType.PerspectiveSet:
					flag = metadataObject.Parent != null;
					break;
				case ObjectType.Role:
				case ObjectType.RoleMembership:
				case ObjectType.Variation:
				case ObjectType.Set:
				case ObjectType.ExtendedProperty:
				case ObjectType.Expression:
					goto IL_00C8;
				case ObjectType.TablePermission:
					flag = true;
					break;
				case ObjectType.ColumnPermission:
					flag = metadataObject.Parent != null;
					break;
				default:
					if (objectType != ObjectType.CalendarColumnReference)
					{
						goto IL_00C8;
					}
					flag = metadataObject.Parent != null && metadataObject.Parent.Parent != null && metadataObject.Parent.Parent.Parent != null;
					break;
				}
				IL_00CA:
				if (!flag || !metadataObject.BuildIndirectNameCrossLinkPathIfNeeded())
				{
					i++;
					continue;
				}
				this.objectsRegisteredForCrossLinkReconstruction.RemoveAt(i);
				continue;
				IL_00C8:
				flag = false;
				goto IL_00CA;
			}
			bool flag2 = this.objectsRegisteredForCrossLinkReconstruction.Count == 0;
			if (!keepObjectsWithUnresolvedReference)
			{
				this.objectsRegisteredForCrossLinkReconstruction.Clear();
			}
			return flag2;
		}

		// Token: 0x04000496 RID: 1174
		private List<MetadataObject> objectsRegisteredForCrossLinkReconstruction;
	}
}
