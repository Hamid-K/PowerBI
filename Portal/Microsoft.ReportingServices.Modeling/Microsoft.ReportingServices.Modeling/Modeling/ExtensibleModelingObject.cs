using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000051 RID: 81
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ExtensibleModelingObject : ModelingObject
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000B24D File Offset: 0x0000944D
		protected ExtensibleModelingObject()
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000B255 File Offset: 0x00009455
		internal ExtensibleModelingObject(ExtensibleModelingObject objectToCopy)
		{
			if (objectToCopy.m_customProperties != null)
			{
				this.m_customProperties = objectToCopy.m_customProperties.Clone();
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000B276 File Offset: 0x00009476
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000B2A4 File Offset: 0x000094A4
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customProperties == null)
				{
					if (base.IsCompiled)
					{
						throw new InternalModelingException("Object is compiled and m_customProperties is null");
					}
					this.m_customProperties = new CustomPropertyCollection();
				}
				return this.m_customProperties;
			}
			internal set
			{
				base.CheckWriteable();
				this.m_customProperties = value;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000B2B3 File Offset: 0x000094B3
		internal void WriteCustomProperties(ModelingXmlWriter xw)
		{
			if (this.m_customProperties != null)
			{
				this.m_customProperties.WriteTo(xw);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000B2C9 File Offset: 0x000094C9
		protected override void Compile(bool shouldPersist)
		{
			base.Compile(shouldPersist);
			if (shouldPersist)
			{
				if (this.m_customProperties != null)
				{
					this.m_customProperties.MakeReadOnly();
					return;
				}
				this.m_customProperties = CustomPropertyCollection.Empty;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000B2F4 File Offset: 0x000094F4
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ExtensibleModelingObject.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.CustomProperties)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				((IPersistable)this.CustomProperties).Serialize(writer);
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000B368 File Offset: 0x00009568
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (base.AllowWriteOperations())
			{
				reader.RegisterDeclaration(ExtensibleModelingObject.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.CustomProperties)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					((IPersistable)this.CustomProperties).Deserialize(reader);
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000B400 File Offset: 0x00009600
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ExtensibleModelingObject.__declaration, ExtensibleModelingObject.__declarationLock, () => new Declaration(ObjectType.ExtensibleModelingObject, ObjectType.ModelingObject, new List<MemberInfo>
				{
					new MemberInfo(MemberName.CustomProperties, ObjectType.CustomPropertyCollection)
				}));
			}
		}

		// Token: 0x040001FD RID: 509
		private CustomPropertyCollection m_customProperties;

		// Token: 0x040001FE RID: 510
		private static Declaration __declaration;

		// Token: 0x040001FF RID: 511
		private static readonly object __declarationLock = new object();
	}
}
