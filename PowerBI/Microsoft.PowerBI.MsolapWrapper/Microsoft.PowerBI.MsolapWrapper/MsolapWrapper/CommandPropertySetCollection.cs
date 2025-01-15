using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x02000008 RID: 8
	internal class CommandPropertySetCollection : PropertySetCollection
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00002C50 File Offset: 0x00002050
		internal CommandPropertySetCollection()
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003348 File Offset: 0x00002748
		internal unsafe void AddProperty(CommandProperties prop, object value)
		{
			if (!this._propertiesAlreadySet.Contains((int)prop))
			{
				switch (prop)
				{
				case CommandProperties.ForwardOnly:
				{
					bool flag = (bool)value;
					if (flag)
					{
						PropertySet propertySet = base.GetPropertySet(0);
						propertySet.Add(new Property(0, <Module>.DB_NULLID, 14, 0));
						propertySet.Add(new Property(1, <Module>.DB_NULLID, 18, 0));
						propertySet.Add(new Property(1, <Module>.DB_NULLID, 21, 0));
					}
					break;
				}
				case CommandProperties.MemoryLimit:
				{
					PropertySet propertySet2 = base.GetPropertySet(1);
					int num = (int)value;
					propertySet2.Add(new Property(0, <Module>.DB_NULLID, 4209, num));
					break;
				}
				case CommandProperties.CommandTimeout:
				{
					PropertySet propertySet3 = base.GetPropertySet(0);
					int num2 = (int)value;
					propertySet3.Add(new Property(0, <Module>.DB_NULLID, 34, num2));
					break;
				}
				case CommandProperties.ActivityId:
				{
					PropertySet propertySet4 = base.GetPropertySet(1);
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
					propertySet4.Add(new Property(0, db_NULLID, 4181, (CComBSTR*)ptr));
					break;
				}
				case CommandProperties.CurrentActivityId:
				{
					PropertySet propertySet5 = base.GetPropertySet(1);
					string text2 = (string)value;
					CComBSTR ccomBSTR2;
					CComBSTR* ptr2 = Utils.StringToBSTR(&ccomBSTR2, text2);
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
					propertySet5.Add(new Property(0, db_NULLID2, 4210, (CComBSTR*)ptr2));
					break;
				}
				case CommandProperties.RequestId:
				{
					PropertySet propertySet6 = base.GetPropertySet(1);
					string text3 = (string)value;
					CComBSTR ccomBSTR3;
					CComBSTR* ptr3 = Utils.StringToBSTR(&ccomBSTR3, text3);
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
					propertySet6.Add(new Property(0, db_NULLID3, 4182, (CComBSTR*)ptr3));
					break;
				}
				case CommandProperties.RequestPriority:
				{
					PropertySet propertySet7 = base.GetPropertySet(1);
					int num3 = (int)value;
					propertySet7.Add(new Property(0, <Module>.DB_NULLID, 4221, num3));
					break;
				}
				case CommandProperties.ExecutionMetrics:
				{
					PropertySet propertySet8 = base.GetPropertySet(1);
					int num4 = (int)value;
					propertySet8.Add(new Property(0, <Module>.DB_NULLID, 4224, num4));
					break;
				}
				case CommandProperties.ApplicationContext:
				{
					PropertySet propertySet9 = base.GetPropertySet(1);
					string text4 = (string)value;
					CComBSTR ccomBSTR4;
					CComBSTR* ptr4 = Utils.StringToBSTR(&ccomBSTR4, text4);
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
					propertySet9.Add(new Property(0, db_NULLID4, 4225, (CComBSTR*)ptr4));
					break;
				}
				default:
					WrapperContract.Fail("Unknown property.");
					break;
				}
				this._propertiesAlreadySet.Add((int)prop);
				GC.KeepAlive(this);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002448 File Offset: 0x00001848
		[return: MarshalAs(UnmanagedType.U1)]
		internal bool HasForwardOnly()
		{
			return this._propertiesAlreadySet.Contains(0);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002468 File Offset: 0x00001868
		private void !CommandPropertySetCollection()
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002468 File Offset: 0x00001868
		private void ~CommandPropertySetCollection()
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000029AC File Offset: 0x00001DAC
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
