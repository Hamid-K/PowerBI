using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CC7 RID: 7367
	internal class FirewallPartitionPlan : IFirewallPartitionPlan
	{
		// Token: 0x0600B798 RID: 47000 RVA: 0x000020FD File Offset: 0x000002FD
		public FirewallPartitionPlan()
		{
		}

		// Token: 0x0600B799 RID: 47001 RVA: 0x002540EC File Offset: 0x002522EC
		public FirewallPartitionPlan(IPartitionKey partitionKey, int evaluationOrder, IEnumerable<IPartitionKey> inputs)
		{
			this.partitionKey = partitionKey;
			this.evaluationOrder = evaluationOrder;
			this.inputs = inputs.ToArray<IPartitionKey>();
			this.resources = new List<IResource>();
		}

		// Token: 0x17002DA6 RID: 11686
		// (get) Token: 0x0600B79A RID: 47002 RVA: 0x00254119 File Offset: 0x00252319
		public IPartitionKey PartitionKey
		{
			get
			{
				return this.partitionKey;
			}
		}

		// Token: 0x17002DA7 RID: 11687
		// (get) Token: 0x0600B79B RID: 47003 RVA: 0x00254121 File Offset: 0x00252321
		public int EvaluationOrder
		{
			get
			{
				return this.evaluationOrder;
			}
		}

		// Token: 0x17002DA8 RID: 11688
		// (get) Token: 0x0600B79C RID: 47004 RVA: 0x00254129 File Offset: 0x00252329
		// (set) Token: 0x0600B79D RID: 47005 RVA: 0x00254131 File Offset: 0x00252331
		public bool IsCyclic
		{
			get
			{
				return this.isCyclic;
			}
			set
			{
				this.isCyclic = value;
			}
		}

		// Token: 0x17002DA9 RID: 11689
		// (get) Token: 0x0600B79E RID: 47006 RVA: 0x0025413A File Offset: 0x0025233A
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17002DAA RID: 11690
		// (get) Token: 0x0600B79F RID: 47007 RVA: 0x00254142 File Offset: 0x00252342
		public IEnumerable<IPartitionKey> Inputs
		{
			get
			{
				return this.inputs;
			}
		}

		// Token: 0x17002DAB RID: 11691
		// (get) Token: 0x0600B7A0 RID: 47008 RVA: 0x0025414A File Offset: 0x0025234A
		public IEnumerable<IResource> Resources
		{
			get
			{
				return this.resources;
			}
		}

		// Token: 0x0600B7A1 RID: 47009 RVA: 0x00254152 File Offset: 0x00252352
		public void AddException(Exception exception)
		{
			if (this.exception == null)
			{
				this.exception = exception;
			}
		}

		// Token: 0x0600B7A2 RID: 47010 RVA: 0x00254163 File Offset: 0x00252363
		public void AddResources(IEnumerable<IResource> resources)
		{
			this.resources.AddRange(resources);
		}

		// Token: 0x0600B7A3 RID: 47011 RVA: 0x00254174 File Offset: 0x00252374
		public void Serialize(BinaryWriter writer)
		{
			writer.WriteIPartitionKey(this.partitionKey);
			writer.WriteInt32(this.evaluationOrder);
			writer.WriteArray(this.inputs, delegate(BinaryWriter w, IPartitionKey i)
			{
				w.WriteIPartitionKey(i);
			});
			writer.WriteList(this.resources, delegate(BinaryWriter w, IResource i)
			{
				w.WriteIResource(i);
			});
			writer.WriteBool(this.isCyclic);
			writer.WriteBool(this.exception != null);
			if (this.exception != null)
			{
				writer.WriteException(this.exception);
			}
		}

		// Token: 0x0600B7A4 RID: 47012 RVA: 0x00254220 File Offset: 0x00252420
		public void Deserialize(BinaryReader reader)
		{
			this.partitionKey = reader.ReadIPartitionKey();
			this.evaluationOrder = reader.ReadInt32();
			this.inputs = reader.ReadArray((BinaryReader r) => r.ReadIPartitionKey());
			this.resources = reader.ReadList((BinaryReader r) => r.ReadIResource());
			this.isCyclic = reader.ReadBool();
			if (reader.ReadBool())
			{
				this.exception = reader.ReadException();
			}
		}

		// Token: 0x04005DAC RID: 23980
		private IPartitionKey partitionKey;

		// Token: 0x04005DAD RID: 23981
		private int evaluationOrder;

		// Token: 0x04005DAE RID: 23982
		private IPartitionKey[] inputs;

		// Token: 0x04005DAF RID: 23983
		private List<IResource> resources;

		// Token: 0x04005DB0 RID: 23984
		private bool isCyclic;

		// Token: 0x04005DB1 RID: 23985
		private Exception exception;
	}
}
