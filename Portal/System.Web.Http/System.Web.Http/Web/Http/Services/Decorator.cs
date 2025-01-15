using System;

namespace System.Web.Http.Services
{
	// Token: 0x020000A4 RID: 164
	public static class Decorator
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000B6E4 File Offset: 0x000098E4
		public static T GetInner<T>(T outer)
		{
			T t = outer;
			IDecorator<T> decorator2;
			for (IDecorator<T> decorator = t as IDecorator<T>; decorator != null; decorator = decorator2)
			{
				t = decorator.Inner;
				decorator2 = t as IDecorator<T>;
				if (decorator == decorator2)
				{
					break;
				}
			}
			return t;
		}
	}
}
