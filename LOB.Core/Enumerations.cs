using Attribute = System.Attribute;

namespace LOB.Core
{
    public enum ItemState
    {
        [Enum("Все")] None = 0,
        [Enum("Создан")] Created = 1
    }

    public enum UserState
    {
        [Enum("Все")] None = 0,
        [Enum("Онлайн")] Online = 1, // ready for accepting tasks
        [Enum("Назначено")] Assigned = 2, // a task is assigned but no confirmation yet
        [Enum("Принято")] Accepted = 3, // working on task
        [Enum("Оффлайн")] Offline = 4 // unready to process tasks
    }

    public enum AccountState
    {
        [Enum("Все")] None = 0,
        [Enum("Разрешено")] Enabled = 1, // user is allowed to login
        [Enum("Запрещено")] Disabled = 2 // user login restricted
    }

    public enum RoleType
    {
        [Enum("Все")] None = 0,
        [Enum("Администраторы")] Administrators = 1,
        [Enum("Сотрудники")] Employees = 2,
        [Enum("Пользователи")] Users = 3
    }

    public enum ImageSize
    {
        [Enum("По умолчанию")] None = 0,
        [Enum("Маленькая")] Small = 1,
        [Enum("Средняя")] Medium = 2,
        [Enum("Большая")] Large = 3
    }
}