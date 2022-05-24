using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWPApp.Assets.Access
{
    /// <summary>
    /// Роли и их ID
    /// </summary>
    enum Roles
    {
        /// <summary>
        /// Медицинский персонал
        /// </summary>
        Doctor = 2,

        /// <summary>
        /// Главврач
        /// </summary>
        MainDoctor = 1,

        /// <summary>
        /// Администратор
        /// </summary>
        Administrator = 3,

        /// <summary>
        /// Системная роль
        /// </summary>
        System = 0,

        /// <summary>
        /// Архивариус
        /// </summary>
        Archivist = 4,
    }
}
