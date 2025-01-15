using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000017 RID: 23
	internal class PersistenceValidator
	{
		// Token: 0x06000085 RID: 133 RVA: 0x0000360C File Offset: 0x0000180C
		internal static bool CheckSpecialCase(MemberInfo currentMember, ObjectType persistedType)
		{
			return false;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000360F File Offset: 0x0000180F
		[Conditional("DEBUG")]
		internal static void VerifyReadOrWrite(MemberInfo CurrentMember, PersistMethod persistMethod)
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003614 File Offset: 0x00001814
		[Conditional("DEBUG")]
		internal static void VerifyReadOrWrite(MemberInfo currentMember, PersistMethod persistMethod, Token primitiveType, ObjectType containedType)
		{
			switch (persistMethod)
			{
			case PersistMethod.PrimitiveGenericList:
			case PersistMethod.PrimitiveList:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.PrimitiveList);
				Global.Tracer.Assert(currentMember.Token == primitiveType);
				return;
			case PersistMethod.PrimitiveArray:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.PrimitiveArray);
				Global.Tracer.Assert(currentMember.Token == primitiveType || currentMember.ContainedType == containedType);
				return;
			case PersistMethod.PrimitiveTypedArray:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.PrimitiveTypedArray);
				Global.Tracer.Assert(currentMember.Token == primitiveType);
				return;
			case PersistMethod.GenericListOfReferences:
			case PersistMethod.ListOfReferences:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.RIFObjectList);
				Global.Tracer.Assert(currentMember.Token == Token.Reference);
				return;
			case PersistMethod.GenericListOfGlobalReferences:
			case PersistMethod.ListOfGlobalReferences:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.RIFObjectList);
				Global.Tracer.Assert(currentMember.Token == Token.GlobalReference);
				return;
			case PersistMethod.SerializableArray:
				Global.Tracer.Assert(currentMember.ObjectType == ObjectType.SerializableArray);
				Global.Tracer.Assert(currentMember.Token == Token.Serializable);
				return;
			}
			Global.Tracer.Assert(false, string.Format("ReportIntermediateFormat.Persistence does not support {0}.{1}.", persistMethod.GetType(), persistMethod));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003775 File Offset: 0x00001975
		[Conditional("DEBUG")]
		internal static void VerifyDeclaredType(MemberInfo currentMember, ObjectType persistedType, Dictionary<ObjectType, Declaration> declarations, bool verify)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000377C File Offset: 0x0000197C
		[Conditional("DEBUG")]
		internal static void VerifyDeclaredType(MemberInfo currentMember, ObjectType persistedType, Dictionary<ObjectType, Declaration> declarations)
		{
			if (declarations == null)
			{
				return;
			}
			if (currentMember.ContainedType == ObjectType.RIFObjectArray || currentMember.ContainedType == ObjectType.RIFObjectList)
			{
				return;
			}
			if (!PersistenceValidator.VerifyDeclaredType(currentMember.ObjectType, persistedType, declarations) && !PersistenceValidator.VerifyDeclaredType(currentMember.ContainedType, persistedType, declarations) && !PersistenceValidator.CheckSpecialCase(currentMember, persistedType))
			{
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000037D4 File Offset: 0x000019D4
		private static bool VerifyDeclaredType(ObjectType declaredType, ObjectType persistedType, Dictionary<ObjectType, Declaration> declarations)
		{
			if (persistedType == declaredType || declaredType == ObjectType.RIFObject)
			{
				return true;
			}
			Declaration declaration;
			if (declarations.TryGetValue(persistedType, out declaration))
			{
				while (declaration.BaseObjectType != ObjectType.None)
				{
					if (declaration.BaseObjectType == declaredType)
					{
						return true;
					}
					declaration = declarations[declaration.BaseObjectType];
				}
				return false;
			}
			return true;
		}
	}
}
