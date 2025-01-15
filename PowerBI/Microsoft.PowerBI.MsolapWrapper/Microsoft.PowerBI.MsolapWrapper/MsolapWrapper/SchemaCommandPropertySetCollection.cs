using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x02000009 RID: 9
	internal class SchemaCommandPropertySetCollection : PropertySetCollection
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00002C50 File Offset: 0x00002050
		internal SchemaCommandPropertySetCollection()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003698 File Offset: 0x00002A98
		internal unsafe void AddProperty(SchemaCommandProperties prop, object value)
		{
			if (!this._propertiesAlreadySet.Contains((int)prop))
			{
				if (prop != SchemaCommandProperties.ActivityId)
				{
					if (prop != SchemaCommandProperties.CommandTimeout)
					{
						if (prop != SchemaCommandProperties.CurrentActivityId)
						{
							if (prop != SchemaCommandProperties.RequestId)
							{
								if (prop != SchemaCommandProperties.ApplicationContext)
								{
									WrapperContract.Fail("Unknown property.");
								}
								else
								{
									PropertySet propertySet = base.GetPropertySet(1);
									string text = (string)value;
									CComBSTR ccomBSTR;
									CComBSTR* ptr = Utils.StringToBSTR(&ccomBSTR, text);
									tagDBID db_NULLID;
									try
									{
										db_NULLID = <Module>.DB_NULLID;
									}
									catch
									{
										<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR));
										throw;
									}
									propertySet.Add(new Property(0, db_NULLID, 4225, (CComBSTR*)ptr));
								}
							}
							else
							{
								PropertySet propertySet = base.GetPropertySet(1);
								string text = (string)value;
								CComBSTR ccomBSTR2;
								CComBSTR* ptr2 = Utils.StringToBSTR(&ccomBSTR2, text);
								tagDBID db_NULLID2;
								try
								{
									db_NULLID2 = <Module>.DB_NULLID;
								}
								catch
								{
									<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR2));
									throw;
								}
								propertySet.Add(new Property(0, db_NULLID2, 4182, (CComBSTR*)ptr2));
							}
						}
						else
						{
							PropertySet propertySet = base.GetPropertySet(1);
							string text = (string)value;
							CComBSTR ccomBSTR3;
							CComBSTR* ptr3 = Utils.StringToBSTR(&ccomBSTR3, text);
							tagDBID db_NULLID3;
							try
							{
								db_NULLID3 = <Module>.DB_NULLID;
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR3));
								throw;
							}
							propertySet.Add(new Property(0, db_NULLID3, 4210, (CComBSTR*)ptr3));
						}
					}
					else
					{
						PropertySet propertySet = base.GetPropertySet(0);
						propertySet.Add(new Property(0, <Module>.DB_NULLID, 34, (int)value));
					}
				}
				else
				{
					PropertySet propertySet = base.GetPropertySet(1);
					string text = (string)value;
					CComBSTR ccomBSTR4;
					CComBSTR* ptr4 = Utils.StringToBSTR(&ccomBSTR4, text);
					tagDBID db_NULLID4;
					try
					{
						db_NULLID4 = <Module>.DB_NULLID;
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR4));
						throw;
					}
					propertySet.Add(new Property(0, db_NULLID4, 4181, (CComBSTR*)ptr4));
				}
				this._propertiesAlreadySet.Add((int)prop);
				GC.KeepAlive(this);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00002468 File Offset: 0x00001868
		private void !SchemaCommandPropertySetCollection()
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00002468 File Offset: 0x00001868
		private void ~SchemaCommandPropertySetCollection()
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00002A10 File Offset: 0x00001E10
		[HandleProcessCorruptedStateExceptions]
		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			try
			{
			}
			finally
			{
				base.Dispose(false);
			}
		}
	}
}
