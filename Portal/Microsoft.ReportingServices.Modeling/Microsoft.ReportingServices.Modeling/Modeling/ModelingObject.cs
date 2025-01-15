using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000050 RID: 80
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ModelingObject
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B0EE File Offset: 0x000092EE
		public bool IsCompiled
		{
			get
			{
				return this.m_compiled;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B0F6 File Offset: 0x000092F6
		internal void CheckWriteable()
		{
			if (this.m_compiled)
			{
				throw new InvalidOperationException(DevExceptionMessages.ReadOnly);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B10B File Offset: 0x0000930B
		protected virtual void Compile(bool shouldPersist)
		{
			if (shouldPersist)
			{
				this.SetCompiledIndicator();
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B116 File Offset: 0x00009316
		protected void SetCompiledIndicator()
		{
			this.CheckWriteable();
			this.m_compiled = true;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B125 File Offset: 0x00009325
		internal IDisposable AllowWriteOperations()
		{
			return new ModelingObject.AllowWriteOperationsImpl(this);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B134 File Offset: 0x00009334
		internal virtual void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ModelingObject.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Compiled)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				writer.Write(this.m_compiled);
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000B1A4 File Offset: 0x000093A4
		internal virtual void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ModelingObject.Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName != MemberName.Compiled)
				{
					throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
				}
				this.m_compiled = reader.ReadBoolean();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000B211 File Offset: 0x00009411
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelingObject.__declaration, ModelingObject.__declarationLock, () => new Declaration(ObjectType.ModelingObject, ObjectType.RefHelper, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Compiled, Token.Boolean)
				}));
			}
		}

		// Token: 0x040001FA RID: 506
		private bool m_compiled;

		// Token: 0x040001FB RID: 507
		private static Declaration __declaration;

		// Token: 0x040001FC RID: 508
		private static readonly object __declarationLock = new object();

		// Token: 0x02000125 RID: 293
		private struct AllowWriteOperationsImpl : IDisposable
		{
			// Token: 0x06000DCC RID: 3532 RVA: 0x0002D206 File Offset: 0x0002B406
			internal AllowWriteOperationsImpl(ModelingObject modelingObject)
			{
				this.m_modelingObject = modelingObject;
				this.m_savedIsCompiled = modelingObject.m_compiled;
				if (modelingObject.m_compiled)
				{
					modelingObject.m_compiled = false;
				}
			}

			// Token: 0x06000DCD RID: 3533 RVA: 0x0002D22A File Offset: 0x0002B42A
			public void Dispose()
			{
				this.m_modelingObject.m_compiled = this.m_savedIsCompiled;
			}

			// Token: 0x040005B9 RID: 1465
			private readonly ModelingObject m_modelingObject;

			// Token: 0x040005BA RID: 1466
			private readonly bool m_savedIsCompiled;
		}
	}
}
