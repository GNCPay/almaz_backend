﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ALMAZ")]
	public partial class CMSDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCardType(CardType instance);
    partial void UpdateCardType(CardType instance);
    partial void DeleteCardType(CardType instance);
    partial void InsertCardTrace(CardTrace instance);
    partial void UpdateCardTrace(CardTrace instance);
    partial void DeleteCardTrace(CardTrace instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertCardActionLog(CardActionLog instance);
    partial void UpdateCardActionLog(CardActionLog instance);
    partial void DeleteCardActionLog(CardActionLog instance);
    partial void InsertCard(Card instance);
    partial void UpdateCard(Card instance);
    partial void DeleteCard(Card instance);
    #endregion
		
		public CMSDBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["ALMAZConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CMSDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CMSDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CMSDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CMSDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CardType> CardTypes
		{
			get
			{
				return this.GetTable<CardType>();
			}
		}
		
		public System.Data.Linq.Table<CardTrace> CardTraces
		{
			get
			{
				return this.GetTable<CardTrace>();
			}
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<CardActionLog> CardActionLogs
		{
			get
			{
				return this.GetTable<CardActionLog>();
			}
		}
		
		public System.Data.Linq.Table<Card> Cards
		{
			get
			{
				return this.GetTable<Card>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CardTypes")]
	public partial class CardType : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _TypeCode;
		
		private string _TypeName;
		
		private long _PrepaidValue;
		
		private long _AddValue;
		
		private EntitySet<Card> _Cards;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTypeCodeChanging(string value);
    partial void OnTypeCodeChanged();
    partial void OnTypeNameChanging(string value);
    partial void OnTypeNameChanged();
    partial void OnPrepaidValueChanging(long value);
    partial void OnPrepaidValueChanged();
    partial void OnAddValueChanging(long value);
    partial void OnAddValueChanged();
    #endregion
		
		public CardType()
		{
			this._Cards = new EntitySet<Card>(new Action<Card>(this.attach_Cards), new Action<Card>(this.detach_Cards));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TypeCode", DbType="VarChar(20) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string TypeCode
		{
			get
			{
				return this._TypeCode;
			}
			set
			{
				if ((this._TypeCode != value))
				{
					this.OnTypeCodeChanging(value);
					this.SendPropertyChanging();
					this._TypeCode = value;
					this.SendPropertyChanged("TypeCode");
					this.OnTypeCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TypeName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string TypeName
		{
			get
			{
				return this._TypeName;
			}
			set
			{
				if ((this._TypeName != value))
				{
					this.OnTypeNameChanging(value);
					this.SendPropertyChanging();
					this._TypeName = value;
					this.SendPropertyChanged("TypeName");
					this.OnTypeNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PrepaidValue", DbType="BigInt NOT NULL")]
		public long PrepaidValue
		{
			get
			{
				return this._PrepaidValue;
			}
			set
			{
				if ((this._PrepaidValue != value))
				{
					this.OnPrepaidValueChanging(value);
					this.SendPropertyChanging();
					this._PrepaidValue = value;
					this.SendPropertyChanged("PrepaidValue");
					this.OnPrepaidValueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddValue", DbType="BigInt NOT NULL")]
		public long AddValue
		{
			get
			{
				return this._AddValue;
			}
			set
			{
				if ((this._AddValue != value))
				{
					this.OnAddValueChanging(value);
					this.SendPropertyChanging();
					this._AddValue = value;
					this.SendPropertyChanged("AddValue");
					this.OnAddValueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CardType_Card", Storage="_Cards", ThisKey="TypeCode", OtherKey="CardType")]
		public EntitySet<Card> Cards
		{
			get
			{
				return this._Cards;
			}
			set
			{
				this._Cards.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Cards(Card entity)
		{
			this.SendPropertyChanging();
			entity.CardType1 = this;
		}
		
		private void detach_Cards(Card entity)
		{
			this.SendPropertyChanging();
			entity.CardType1 = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CardTraces")]
	public partial class CardTrace : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private string _CardTrace1;
		
		private string _CardNumber;
		
		private string _CustomerFullName;
		
		private string _UserId;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnCardTrace1Changing(string value);
    partial void OnCardTrace1Changed();
    partial void OnCardNumberChanging(string value);
    partial void OnCardNumberChanged();
    partial void OnCustomerFullNameChanging(string value);
    partial void OnCustomerFullNameChanged();
    partial void OnUserIdChanging(string value);
    partial void OnUserIdChanged();
    #endregion
		
		public CardTrace()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="CardTrace", Storage="_CardTrace1", DbType="NText NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string CardTrace1
		{
			get
			{
				return this._CardTrace1;
			}
			set
			{
				if ((this._CardTrace1 != value))
				{
					this.OnCardTrace1Changing(value);
					this.SendPropertyChanging();
					this._CardTrace1 = value;
					this.SendPropertyChanged("CardTrace1");
					this.OnCardTrace1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardNumber", DbType="NVarChar(50)")]
		public string CardNumber
		{
			get
			{
				return this._CardNumber;
			}
			set
			{
				if ((this._CardNumber != value))
				{
					this.OnCardNumberChanging(value);
					this.SendPropertyChanging();
					this._CardNumber = value;
					this.SendPropertyChanged("CardNumber");
					this.OnCardNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerFullName", DbType="NVarChar(50)")]
		public string CustomerFullName
		{
			get
			{
				return this._CustomerFullName;
			}
			set
			{
				if ((this._CustomerFullName != value))
				{
					this.OnCustomerFullNameChanging(value);
					this.SendPropertyChanging();
					this._CustomerFullName = value;
					this.SendPropertyChanged("CustomerFullName");
					this.OnCustomerFullNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="NVarChar(50)")]
		public string UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _UserId;
		
		private string _FullName;
		
		private string _CardID;
		
		private string _Password;
		
		private string _Role;
		
		private bool _IsActived;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(string value);
    partial void OnUserIdChanged();
    partial void OnFullNameChanging(string value);
    partial void OnFullNameChanged();
    partial void OnCardIDChanging(string value);
    partial void OnCardIDChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnRoleChanging(string value);
    partial void OnRoleChanged();
    partial void OnIsActivedChanging(bool value);
    partial void OnIsActivedChanged();
    #endregion
		
		public User()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FullName", DbType="NVarChar(100)")]
		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				if ((this._FullName != value))
				{
					this.OnFullNameChanging(value);
					this.SendPropertyChanging();
					this._FullName = value;
					this.SendPropertyChanged("FullName");
					this.OnFullNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardID", DbType="VarChar(32)")]
		public string CardID
		{
			get
			{
				return this._CardID;
			}
			set
			{
				if ((this._CardID != value))
				{
					this.OnCardIDChanging(value);
					this.SendPropertyChanging();
					this._CardID = value;
					this.SendPropertyChanged("CardID");
					this.OnCardIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(255)")]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Role", DbType="VarChar(32)")]
		public string Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				if ((this._Role != value))
				{
					this.OnRoleChanging(value);
					this.SendPropertyChanging();
					this._Role = value;
					this.SendPropertyChanged("Role");
					this.OnRoleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsActived", DbType="Bit NOT NULL")]
		public bool IsActived
		{
			get
			{
				return this._IsActived;
			}
			set
			{
				if ((this._IsActived != value))
				{
					this.OnIsActivedChanging(value);
					this.SendPropertyChanging();
					this._IsActived = value;
					this.SendPropertyChanged("IsActived");
					this.OnIsActivedChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CardActionLogs")]
	public partial class CardActionLog : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private System.DateTime _ActionTime;
		
		private string _CardId;
		
		private string _ActionCode;
		
		private string _ActionBy;
		
		private string _ActionAt;
		
		private string _ReferenceID;
		
		private System.Nullable<long> _StartBalance;
		
		private System.Nullable<long> _EndBalance;
		
		private System.Nullable<long> _Amount;
		
		private string _Note;
		
		private EntityRef<Card> _Card;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnActionTimeChanging(System.DateTime value);
    partial void OnActionTimeChanged();
    partial void OnCardIdChanging(string value);
    partial void OnCardIdChanged();
    partial void OnActionCodeChanging(string value);
    partial void OnActionCodeChanged();
    partial void OnActionByChanging(string value);
    partial void OnActionByChanged();
    partial void OnActionAtChanging(string value);
    partial void OnActionAtChanged();
    partial void OnReferenceIDChanging(string value);
    partial void OnReferenceIDChanged();
    partial void OnStartBalanceChanging(System.Nullable<long> value);
    partial void OnStartBalanceChanged();
    partial void OnEndBalanceChanging(System.Nullable<long> value);
    partial void OnEndBalanceChanged();
    partial void OnAmountChanging(System.Nullable<long> value);
    partial void OnAmountChanged();
    partial void OnNoteChanging(string value);
    partial void OnNoteChanged();
    #endregion
		
		public CardActionLog()
		{
			this._Card = default(EntityRef<Card>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActionTime", DbType="DateTime NOT NULL")]
		public System.DateTime ActionTime
		{
			get
			{
				return this._ActionTime;
			}
			set
			{
				if ((this._ActionTime != value))
				{
					this.OnActionTimeChanging(value);
					this.SendPropertyChanging();
					this._ActionTime = value;
					this.SendPropertyChanged("ActionTime");
					this.OnActionTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardId", DbType="VarChar(32) NOT NULL", CanBeNull=false)]
		public string CardId
		{
			get
			{
				return this._CardId;
			}
			set
			{
				if ((this._CardId != value))
				{
					if (this._Card.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCardIdChanging(value);
					this.SendPropertyChanging();
					this._CardId = value;
					this.SendPropertyChanged("CardId");
					this.OnCardIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActionCode", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ActionCode
		{
			get
			{
				return this._ActionCode;
			}
			set
			{
				if ((this._ActionCode != value))
				{
					this.OnActionCodeChanging(value);
					this.SendPropertyChanging();
					this._ActionCode = value;
					this.SendPropertyChanged("ActionCode");
					this.OnActionCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActionBy", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ActionBy
		{
			get
			{
				return this._ActionBy;
			}
			set
			{
				if ((this._ActionBy != value))
				{
					this.OnActionByChanging(value);
					this.SendPropertyChanging();
					this._ActionBy = value;
					this.SendPropertyChanged("ActionBy");
					this.OnActionByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActionAt", DbType="NVarChar(50)")]
		public string ActionAt
		{
			get
			{
				return this._ActionAt;
			}
			set
			{
				if ((this._ActionAt != value))
				{
					this.OnActionAtChanging(value);
					this.SendPropertyChanging();
					this._ActionAt = value;
					this.SendPropertyChanged("ActionAt");
					this.OnActionAtChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReferenceID", DbType="NVarChar(50)")]
		public string ReferenceID
		{
			get
			{
				return this._ReferenceID;
			}
			set
			{
				if ((this._ReferenceID != value))
				{
					this.OnReferenceIDChanging(value);
					this.SendPropertyChanging();
					this._ReferenceID = value;
					this.SendPropertyChanged("ReferenceID");
					this.OnReferenceIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartBalance", DbType="BigInt")]
		public System.Nullable<long> StartBalance
		{
			get
			{
				return this._StartBalance;
			}
			set
			{
				if ((this._StartBalance != value))
				{
					this.OnStartBalanceChanging(value);
					this.SendPropertyChanging();
					this._StartBalance = value;
					this.SendPropertyChanged("StartBalance");
					this.OnStartBalanceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndBalance", DbType="BigInt")]
		public System.Nullable<long> EndBalance
		{
			get
			{
				return this._EndBalance;
			}
			set
			{
				if ((this._EndBalance != value))
				{
					this.OnEndBalanceChanging(value);
					this.SendPropertyChanging();
					this._EndBalance = value;
					this.SendPropertyChanged("EndBalance");
					this.OnEndBalanceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Amount", DbType="BigInt")]
		public System.Nullable<long> Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if ((this._Amount != value))
				{
					this.OnAmountChanging(value);
					this.SendPropertyChanging();
					this._Amount = value;
					this.SendPropertyChanged("Amount");
					this.OnAmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Note", DbType="NText NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string Note
		{
			get
			{
				return this._Note;
			}
			set
			{
				if ((this._Note != value))
				{
					this.OnNoteChanging(value);
					this.SendPropertyChanging();
					this._Note = value;
					this.SendPropertyChanged("Note");
					this.OnNoteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Card_CardActionLog", Storage="_Card", ThisKey="CardId", OtherKey="CardId", IsForeignKey=true)]
		public Card Card
		{
			get
			{
				return this._Card.Entity;
			}
			set
			{
				Card previousValue = this._Card.Entity;
				if (((previousValue != value) 
							|| (this._Card.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Card.Entity = null;
						previousValue.CardActionLogs.Remove(this);
					}
					this._Card.Entity = value;
					if ((value != null))
					{
						value.CardActionLogs.Add(this);
						this._CardId = value.CardId;
					}
					else
					{
						this._CardId = default(string);
					}
					this.SendPropertyChanged("Card");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Cards")]
	public partial class Card : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _CardId;
		
		private string _CardNumber;
		
		private string _CardType;
		
		private System.Nullable<long> _PrepaidAmount;
		
		private System.Nullable<decimal> _Balance;
		
		private string _CardOwnerName;
		
		private string _CustomerCIF;
		
		private System.Nullable<System.DateTime> _Valid;
		
		private System.Nullable<System.DateTime> _Expire;
		
		private bool _IsActived;
		
		private bool _IsLockout;
		
		private System.DateTime _CreatedDate;
		
		private System.Nullable<System.DateTime> _ActivedDate;
		
		private System.Nullable<System.DateTime> _LockoutDate;
		
		private string _RegisterAt;
		
		private string _CardInfo;
		
		private EntitySet<CardActionLog> _CardActionLogs;
		
		private EntityRef<CardType> _CardType1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCardIdChanging(string value);
    partial void OnCardIdChanged();
    partial void OnCardNumberChanging(string value);
    partial void OnCardNumberChanged();
    partial void OnCardTypeChanging(string value);
    partial void OnCardTypeChanged();
    partial void OnPrepaidAmountChanging(System.Nullable<long> value);
    partial void OnPrepaidAmountChanged();
    partial void OnBalanceChanging(System.Nullable<decimal> value);
    partial void OnBalanceChanged();
    partial void OnCardOwnerNameChanging(string value);
    partial void OnCardOwnerNameChanged();
    partial void OnCustomerCIFChanging(string value);
    partial void OnCustomerCIFChanged();
    partial void OnValidChanging(System.Nullable<System.DateTime> value);
    partial void OnValidChanged();
    partial void OnExpireChanging(System.Nullable<System.DateTime> value);
    partial void OnExpireChanged();
    partial void OnIsActivedChanging(bool value);
    partial void OnIsActivedChanged();
    partial void OnIsLockoutChanging(bool value);
    partial void OnIsLockoutChanged();
    partial void OnCreatedDateChanging(System.DateTime value);
    partial void OnCreatedDateChanged();
    partial void OnActivedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnActivedDateChanged();
    partial void OnLockoutDateChanging(System.Nullable<System.DateTime> value);
    partial void OnLockoutDateChanged();
    partial void OnRegisterAtChanging(string value);
    partial void OnRegisterAtChanged();
    partial void OnCardInfoChanging(string value);
    partial void OnCardInfoChanged();
    #endregion
		
		public Card()
		{
			this._CardActionLogs = new EntitySet<CardActionLog>(new Action<CardActionLog>(this.attach_CardActionLogs), new Action<CardActionLog>(this.detach_CardActionLogs));
			this._CardType1 = default(EntityRef<CardType>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardId", DbType="VarChar(32) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string CardId
		{
			get
			{
				return this._CardId;
			}
			set
			{
				if ((this._CardId != value))
				{
					this.OnCardIdChanging(value);
					this.SendPropertyChanging();
					this._CardId = value;
					this.SendPropertyChanged("CardId");
					this.OnCardIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardNumber", DbType="VarChar(64) NOT NULL", CanBeNull=false)]
		public string CardNumber
		{
			get
			{
				return this._CardNumber;
			}
			set
			{
				if ((this._CardNumber != value))
				{
					this.OnCardNumberChanging(value);
					this.SendPropertyChanging();
					this._CardNumber = value;
					this.SendPropertyChanged("CardNumber");
					this.OnCardNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardType", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string CardType
		{
			get
			{
				return this._CardType;
			}
			set
			{
				if ((this._CardType != value))
				{
					if (this._CardType1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCardTypeChanging(value);
					this.SendPropertyChanging();
					this._CardType = value;
					this.SendPropertyChanged("CardType");
					this.OnCardTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PrepaidAmount", DbType="BigInt")]
		public System.Nullable<long> PrepaidAmount
		{
			get
			{
				return this._PrepaidAmount;
			}
			set
			{
				if ((this._PrepaidAmount != value))
				{
					this.OnPrepaidAmountChanging(value);
					this.SendPropertyChanging();
					this._PrepaidAmount = value;
					this.SendPropertyChanged("PrepaidAmount");
					this.OnPrepaidAmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Balance", DbType="Decimal(18,0)")]
		public System.Nullable<decimal> Balance
		{
			get
			{
				return this._Balance;
			}
			set
			{
				if ((this._Balance != value))
				{
					this.OnBalanceChanging(value);
					this.SendPropertyChanging();
					this._Balance = value;
					this.SendPropertyChanged("Balance");
					this.OnBalanceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardOwnerName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string CardOwnerName
		{
			get
			{
				return this._CardOwnerName;
			}
			set
			{
				if ((this._CardOwnerName != value))
				{
					this.OnCardOwnerNameChanging(value);
					this.SendPropertyChanging();
					this._CardOwnerName = value;
					this.SendPropertyChanged("CardOwnerName");
					this.OnCardOwnerNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerCIF", DbType="VarChar(64)")]
		public string CustomerCIF
		{
			get
			{
				return this._CustomerCIF;
			}
			set
			{
				if ((this._CustomerCIF != value))
				{
					this.OnCustomerCIFChanging(value);
					this.SendPropertyChanging();
					this._CustomerCIF = value;
					this.SendPropertyChanged("CustomerCIF");
					this.OnCustomerCIFChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Valid", DbType="DateTime")]
		public System.Nullable<System.DateTime> Valid
		{
			get
			{
				return this._Valid;
			}
			set
			{
				if ((this._Valid != value))
				{
					this.OnValidChanging(value);
					this.SendPropertyChanging();
					this._Valid = value;
					this.SendPropertyChanged("Valid");
					this.OnValidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Expire", DbType="DateTime")]
		public System.Nullable<System.DateTime> Expire
		{
			get
			{
				return this._Expire;
			}
			set
			{
				if ((this._Expire != value))
				{
					this.OnExpireChanging(value);
					this.SendPropertyChanging();
					this._Expire = value;
					this.SendPropertyChanged("Expire");
					this.OnExpireChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsActived", DbType="Bit NOT NULL")]
		public bool IsActived
		{
			get
			{
				return this._IsActived;
			}
			set
			{
				if ((this._IsActived != value))
				{
					this.OnIsActivedChanging(value);
					this.SendPropertyChanging();
					this._IsActived = value;
					this.SendPropertyChanged("IsActived");
					this.OnIsActivedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsLockout", DbType="Bit NOT NULL")]
		public bool IsLockout
		{
			get
			{
				return this._IsLockout;
			}
			set
			{
				if ((this._IsLockout != value))
				{
					this.OnIsLockoutChanging(value);
					this.SendPropertyChanging();
					this._IsLockout = value;
					this.SendPropertyChanged("IsLockout");
					this.OnIsLockoutChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedDate", DbType="DateTime NOT NULL")]
		public System.DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActivedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> ActivedDate
		{
			get
			{
				return this._ActivedDate;
			}
			set
			{
				if ((this._ActivedDate != value))
				{
					this.OnActivedDateChanging(value);
					this.SendPropertyChanging();
					this._ActivedDate = value;
					this.SendPropertyChanged("ActivedDate");
					this.OnActivedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LockoutDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> LockoutDate
		{
			get
			{
				return this._LockoutDate;
			}
			set
			{
				if ((this._LockoutDate != value))
				{
					this.OnLockoutDateChanging(value);
					this.SendPropertyChanging();
					this._LockoutDate = value;
					this.SendPropertyChanged("LockoutDate");
					this.OnLockoutDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RegisterAt", DbType="NVarChar(50)")]
		public string RegisterAt
		{
			get
			{
				return this._RegisterAt;
			}
			set
			{
				if ((this._RegisterAt != value))
				{
					this.OnRegisterAtChanging(value);
					this.SendPropertyChanging();
					this._RegisterAt = value;
					this.SendPropertyChanged("RegisterAt");
					this.OnRegisterAtChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CardInfo", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string CardInfo
		{
			get
			{
				return this._CardInfo;
			}
			set
			{
				if ((this._CardInfo != value))
				{
					this.OnCardInfoChanging(value);
					this.SendPropertyChanging();
					this._CardInfo = value;
					this.SendPropertyChanged("CardInfo");
					this.OnCardInfoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Card_CardActionLog", Storage="_CardActionLogs", ThisKey="CardId", OtherKey="CardId")]
		public EntitySet<CardActionLog> CardActionLogs
		{
			get
			{
				return this._CardActionLogs;
			}
			set
			{
				this._CardActionLogs.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CardType_Card", Storage="_CardType1", ThisKey="CardType", OtherKey="TypeCode", IsForeignKey=true)]
		public CardType CardType1
		{
			get
			{
				return this._CardType1.Entity;
			}
			set
			{
				CardType previousValue = this._CardType1.Entity;
				if (((previousValue != value) 
							|| (this._CardType1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CardType1.Entity = null;
						previousValue.Cards.Remove(this);
					}
					this._CardType1.Entity = value;
					if ((value != null))
					{
						value.Cards.Add(this);
						this._CardType = value.TypeCode;
					}
					else
					{
						this._CardType = default(string);
					}
					this.SendPropertyChanged("CardType1");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_CardActionLogs(CardActionLog entity)
		{
			this.SendPropertyChanging();
			entity.Card = this;
		}
		
		private void detach_CardActionLogs(CardActionLog entity)
		{
			this.SendPropertyChanging();
			entity.Card = null;
		}
	}
}
#pragma warning restore 1591
