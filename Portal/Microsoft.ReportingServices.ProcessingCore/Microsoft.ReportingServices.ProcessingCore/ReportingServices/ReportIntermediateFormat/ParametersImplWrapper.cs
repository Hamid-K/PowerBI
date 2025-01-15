using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F2 RID: 1266
	[SkipStaticValidation]
	internal class ParametersImplWrapper : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06004062 RID: 16482 RVA: 0x0010FAD7 File Offset: 0x0010DCD7
		internal ParametersImplWrapper()
		{
			this.m_opdParameters = new ParametersImpl();
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x0010FAEA File Offset: 0x0010DCEA
		internal ParametersImplWrapper(ParametersImpl odpParameters)
		{
			this.m_opdParameters = odpParameters;
		}

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x0010FAF9 File Offset: 0x0010DCF9
		internal ParametersImpl WrappedParametersImpl
		{
			get
			{
				return this.m_opdParameters;
			}
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x0010FB04 File Offset: 0x0010DD04
		internal bool ValuesAreEqual(ParametersImplWrapper obj)
		{
			ParameterImpl[] collection = this.m_opdParameters.Collection;
			ParameterImpl[] collection2 = obj.WrappedParametersImpl.Collection;
			if (collection == null)
			{
				return collection2 == null;
			}
			if (collection2 == null)
			{
				return false;
			}
			if (collection.Length != collection2.Length)
			{
				return false;
			}
			for (int i = 0; i < collection.Length; i++)
			{
				if (!collection[i].ValuesAreEqual(collection2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x0010FB60 File Offset: 0x0010DD60
		internal int GetValuesHashCode()
		{
			ParameterImpl[] collection = this.m_opdParameters.Collection;
			if (this.m_hash == 0)
			{
				this.m_hash = 4051;
				if (collection != null)
				{
					this.m_hash |= collection.Length << 16;
					for (int i = 0; i < collection.Length; i++)
					{
						this.m_hash ^= collection[i].GetValuesHashCode();
					}
				}
			}
			return this.m_hash;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x0010FBCC File Offset: 0x0010DDCC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Parameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Parameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Parameter),
				new MemberInfo(MemberName.Names, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringInt32Hashtable, Token.Int32),
				new MemberInfo(MemberName.Count, Token.Int32)
			});
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x0010FC30 File Offset: 0x0010DE30
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ParametersImplWrapper.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Parameters)
				{
					if (memberName != MemberName.Names)
					{
						if (memberName != MemberName.Count)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_opdParameters.Count);
						}
					}
					else
					{
						writer.WriteStringInt32Hashtable(this.m_opdParameters.NameMap);
					}
				}
				else
				{
					ParameterImplWrapper[] array = null;
					if (this.m_opdParameters.Collection != null)
					{
						array = new ParameterImplWrapper[this.m_opdParameters.Collection.Length];
						for (int i = 0; i < array.Length; i++)
						{
							if (this.m_opdParameters.Collection[i] != null)
							{
								array[i] = new ParameterImplWrapper(this.m_opdParameters.Collection[i]);
							}
						}
					}
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array2 = array;
					writer.Write(array2);
				}
			}
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x0010FD14 File Offset: 0x0010DF14
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ParametersImplWrapper.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Parameters)
				{
					if (memberName != MemberName.Names)
					{
						if (memberName != MemberName.Count)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_opdParameters.Count = reader.ReadInt32();
						}
					}
					else
					{
						this.m_opdParameters.NameMap = reader.ReadStringInt32Hashtable<Hashtable>();
					}
				}
				else
				{
					ParameterImplWrapper[] array = reader.ReadArrayOfRIFObjects<ParameterImplWrapper>();
					if (array != null)
					{
						this.m_opdParameters.Collection = new ParameterImpl[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] != null)
							{
								this.m_opdParameters.Collection[i] = array[i].WrappedParameterImpl;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x0010FDDD File Offset: 0x0010DFDD
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x0010FDEA File Offset: 0x0010DFEA
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Parameters;
		}

		// Token: 0x04001D92 RID: 7570
		private ParametersImpl m_opdParameters;

		// Token: 0x04001D93 RID: 7571
		[NonSerialized]
		private int m_hash;

		// Token: 0x04001D94 RID: 7572
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParametersImplWrapper.GetDeclaration();
	}
}
