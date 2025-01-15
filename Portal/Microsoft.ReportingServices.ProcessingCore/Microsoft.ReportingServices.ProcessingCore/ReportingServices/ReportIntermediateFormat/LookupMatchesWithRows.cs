using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004E8 RID: 1256
	internal sealed class LookupMatchesWithRows : LookupMatches
	{
		// Token: 0x06003F78 RID: 16248 RVA: 0x0010D116 File Offset: 0x0010B316
		internal LookupMatchesWithRows()
		{
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x0010D128 File Offset: 0x0010B328
		internal override void AddRow(long rowOffset, int rowIndex, IScalabilityCache scaleCache)
		{
			if (base.HasRow)
			{
				if (this.m_rows == null)
				{
					this.m_rows = new ScalableList<int>(0, scaleCache, 500, 10);
				}
				this.m_rows.Add(rowIndex);
			}
			else
			{
				this.m_firstRow = rowIndex;
			}
			base.AddRow(rowOffset, rowIndex, scaleCache);
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x0010D178 File Offset: 0x0010B378
		internal override void SetupRow(int matchIndex, OnDemandProcessingContext odpContext)
		{
			if (this.m_hasBeenTransferred)
			{
				base.SetupRow(matchIndex, odpContext);
				return;
			}
			CommonRowCache tablixProcessingLookupRowCache = odpContext.TablixProcessingLookupRowCache;
			int num;
			if (matchIndex == 0)
			{
				num = this.m_firstRow;
			}
			else
			{
				num = this.m_rows[matchIndex - 1];
			}
			tablixProcessingLookupRowCache.SetupRow(num, odpContext);
		}

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x0010D1C0 File Offset: 0x0010B3C0
		public override int Size
		{
			get
			{
				return base.Size + 4 + ItemSizes.SizeOf<int>(this.m_rows);
			}
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x0010D1D8 File Offset: 0x0010B3D8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			if (!this.m_hasBeenTransferred)
			{
				writer.RegisterDeclaration(LookupMatchesWithRows.m_Declaration);
				while (writer.NextMember())
				{
					MemberName memberName = writer.CurrentMember.MemberName;
					if (memberName != MemberName.FirstRow)
					{
						if (memberName != MemberName.Rows)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_rows);
						}
					}
					else
					{
						writer.Write(this.m_firstRow);
					}
				}
			}
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x0010D250 File Offset: 0x0010B450
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(LookupMatchesWithRows.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.FirstRow)
				{
					if (memberName != MemberName.Rows)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_rows = reader.ReadRIFObject<ScalableList<int>>();
					}
				}
				else
				{
					this.m_firstRow = reader.ReadInt32();
				}
			}
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x0010D2C0 File Offset: 0x0010B4C0
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x0010D2C2 File Offset: 0x0010B4C2
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			if (this.m_hasBeenTransferred)
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatches;
			}
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatchesWithRows;
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x0010D2D8 File Offset: 0x0010B4D8
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (LookupMatchesWithRows.m_Declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatchesWithRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatches, new List<MemberInfo>
				{
					new MemberInfo(MemberName.FirstRow, Token.Int32),
					new MemberInfo(MemberName.Rows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Token.Int32)
				});
			}
			return LookupMatchesWithRows.m_Declaration;
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x0010D331 File Offset: 0x0010B531
		public override void TransferTo(IScalabilityCache scaleCache)
		{
			base.TransferTo(scaleCache);
			if (this.m_rows != null)
			{
				this.m_rows.Dispose();
				this.m_rows = null;
			}
			this.m_hasBeenTransferred = true;
		}

		// Token: 0x04001D43 RID: 7491
		private int m_firstRow = -1;

		// Token: 0x04001D44 RID: 7492
		private ScalableList<int> m_rows;

		// Token: 0x04001D45 RID: 7493
		[NonSerialized]
		private bool m_hasBeenTransferred;

		// Token: 0x04001D46 RID: 7494
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LookupMatchesWithRows.GetDeclaration();
	}
}
