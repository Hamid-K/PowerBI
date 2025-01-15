using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200022F RID: 559
	internal class TypeContext : LookupContext
	{
		// Token: 0x060012CD RID: 4813 RVA: 0x0002A402 File Offset: 0x00028602
		internal TypeContext(string name, Type type, bool allowNew, bool allowNewArray, IEnvironmentFilter filter)
			: this(name, type, allowNew, allowNewArray, filter, BindingFlags.Default)
		{
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0002A412 File Offset: 0x00028612
		internal TypeContext(string name, Type type, bool allowNew, bool allowNewArray, IEnvironmentFilter filter, BindingFlags additionalFlags)
			: base(name)
		{
			this.m_type = type;
			this.m_allowNew = allowNew;
			this.m_allowNewArray = allowNewArray;
			this.InitMembers(this.m_members, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | additionalFlags, filter);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0002A444 File Offset: 0x00028644
		private void InitMembers(Dictionary<string, MemberContext> memberTable, BindingFlags bindingFlags, IEnvironmentFilter filter)
		{
			MemberInfo[] members = this.m_type.GetMembers(bindingFlags);
			int i = 0;
			while (i < members.Length)
			{
				MemberInfo memberInfo = members[i];
				MemberTypes memberType = memberInfo.MemberType;
				if (memberType == MemberTypes.Field)
				{
					goto IL_0029;
				}
				if (memberType != MemberTypes.Method)
				{
					if (memberType == MemberTypes.Property)
					{
						goto IL_0029;
					}
				}
				else if (!((MethodInfo)memberInfo).IsGenericMethod)
				{
					this.AddMember(memberInfo, memberTable, filter);
				}
				IL_004A:
				i++;
				continue;
				IL_0029:
				this.AddMember(memberInfo, memberTable, filter);
				goto IL_004A;
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0002A4A8 File Offset: 0x000286A8
		private void AddMember(MemberInfo memberDef, Dictionary<string, MemberContext> memberTable, IEnvironmentFilter filter)
		{
			if (!filter.IsAllowedMember(memberDef.Name))
			{
				return;
			}
			MemberContext memberContext;
			if (memberTable.TryGetValue(memberDef.Name, out memberContext))
			{
				memberContext.AddOverload(memberDef);
				return;
			}
			memberContext = new MemberContext(this, memberDef);
			memberTable.Add(memberDef.Name, memberContext);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0002A4F4 File Offset: 0x000286F4
		internal bool IsStandardModule()
		{
			object[] customAttributes = this.m_type.GetCustomAttributes(typeof(StandardModuleAttribute), false);
			return customAttributes != null && customAttributes.Length != 0;
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0002A522 File Offset: 0x00028722
		internal bool AllowNew
		{
			get
			{
				return this.m_allowNew;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0002A52A File Offset: 0x0002872A
		internal bool AllowNewArray
		{
			get
			{
				return this.m_allowNewArray;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0002A532 File Offset: 0x00028732
		internal string MethodFullName
		{
			get
			{
				if (this.m_type != null && !string.IsNullOrEmpty(this.m_type.FullName))
				{
					return this.m_type.FullName;
				}
				return base.Name;
			}
		}

		// Token: 0x040005F6 RID: 1526
		private readonly Type m_type;

		// Token: 0x040005F7 RID: 1527
		private readonly bool m_allowNew;

		// Token: 0x040005F8 RID: 1528
		private readonly bool m_allowNewArray;
	}
}
