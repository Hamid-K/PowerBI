using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003113 RID: 12563
	[DebuggerDisplay("NamespaceValue={NamespaceValue}")]
	internal class AnyParticle : ParticleConstraint
	{
		// Token: 0x0601B3F1 RID: 111601 RVA: 0x0037396C File Offset: 0x00371B6C
		internal AnyParticle()
		{
			this._particleValidator = new AnyParticleValidator(this);
		}

		// Token: 0x170098CD RID: 39117
		// (get) Token: 0x0601B3F2 RID: 111602 RVA: 0x000023C4 File Offset: 0x000005C4
		// (set) Token: 0x0601B3F3 RID: 111603 RVA: 0x0000336E File Offset: 0x0000156E
		internal override ParticleType ParticleType
		{
			get
			{
				return ParticleType.Any;
			}
			set
			{
			}
		}

		// Token: 0x170098CE RID: 39118
		// (set) Token: 0x0601B3F4 RID: 111604 RVA: 0x00373980 File Offset: 0x00371B80
		internal override int ElementId
		{
			set
			{
				this._xsdAnyValue = (ushort)value;
			}
		}

		// Token: 0x170098CF RID: 39119
		// (get) Token: 0x0601B3F5 RID: 111605 RVA: 0x0037398A File Offset: 0x00371B8A
		internal ushort NamespaceValue
		{
			get
			{
				return this._xsdAnyValue;
			}
		}

		// Token: 0x170098D0 RID: 39120
		// (get) Token: 0x0601B3F6 RID: 111606 RVA: 0x00373992 File Offset: 0x00371B92
		internal override IParticleValidator ParticleValidator
		{
			get
			{
				return this._particleValidator;
			}
		}

		// Token: 0x0400B4B4 RID: 46260
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ushort _xsdAnyValue;

		// Token: 0x0400B4B5 RID: 46261
		private IParticleValidator _particleValidator;
	}
}
