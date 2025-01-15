using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002D7 RID: 727
	public sealed class ImageMapAreaInstance : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06001B29 RID: 6953 RVA: 0x0006C503 File Offset: 0x0006A703
		internal ImageMapAreaInstance(ImageMapArea.ImageMapAreaShape shape, float[] coordinates)
			: this(shape, coordinates, null)
		{
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0006C50E File Offset: 0x0006A70E
		internal ImageMapAreaInstance(ImageMapArea.ImageMapAreaShape shape, float[] coordinates, string toolTip)
		{
			this.m_shape = shape;
			this.m_coordinates = coordinates;
			this.m_toolTip = toolTip;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0006C52B File Offset: 0x0006A72B
		internal ImageMapAreaInstance()
		{
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0006C533 File Offset: 0x0006A733
		internal ImageMapAreaInstance(ImageMapArea renderImageMapArea)
		{
			this.m_shape = (ImageMapArea.ImageMapAreaShape)renderImageMapArea.Shape;
			this.m_coordinates = renderImageMapArea.Coordinates;
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0006C553 File Offset: 0x0006A753
		public ImageMapArea.ImageMapAreaShape Shape
		{
			get
			{
				return this.m_shape;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0006C55B File Offset: 0x0006A75B
		public float[] Coordinates
		{
			get
			{
				return this.m_coordinates;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x0006C563 File Offset: 0x0006A763
		public string ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0006C56C File Offset: 0x0006A76C
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ImageMapAreaInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ToolTip)
				{
					if (memberName != MemberName.Shape)
					{
						if (memberName != MemberName.Coordinates)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_coordinates);
						}
					}
					else
					{
						writer.WriteEnum((int)this.m_shape);
					}
				}
				else
				{
					writer.Write(this.m_toolTip);
				}
			}
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0006C5F0 File Offset: 0x0006A7F0
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ImageMapAreaInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ToolTip)
				{
					if (memberName != MemberName.Shape)
					{
						if (memberName != MemberName.Coordinates)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_coordinates = reader.ReadSingleArray();
						}
					}
					else
					{
						this.m_shape = (ImageMapArea.ImageMapAreaShape)reader.ReadEnum();
					}
				}
				else
				{
					this.m_toolTip = reader.ReadString();
				}
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0006C673 File Offset: 0x0006A873
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0006C680 File Offset: 0x0006A880
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageMapAreaInstance;
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0006C688 File Offset: 0x0006A888
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageMapAreaInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Shape, Token.Enum),
				new MemberInfo(MemberName.Coordinates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Single),
				new MemberInfo(MemberName.ToolTip, Token.String)
			});
		}

		// Token: 0x04000D6B RID: 3435
		private ImageMapArea.ImageMapAreaShape m_shape;

		// Token: 0x04000D6C RID: 3436
		private float[] m_coordinates;

		// Token: 0x04000D6D RID: 3437
		private string m_toolTip;

		// Token: 0x04000D6E RID: 3438
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ImageMapAreaInstance.GetDeclaration();
	}
}
