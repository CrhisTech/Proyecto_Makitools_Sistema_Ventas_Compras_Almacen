## 🚀 Sprint 1: Cimientos Arquitectónicos y MVP (Módulos Core)

Durante este primer Sprint, nos enfocamos en establecer la arquitectura base del sistema **Makitools** (Frontend en React + Backend en .NET Core / Entity Framework Core) y desarrollar los flujos de trabajo principales para los módulos de Gestión, Ventas, Compras y Almacén.

Se lograron completar con éxito las siguientes historias de usuario (Issues):

### 🛠️ Gestión del Sistema & Seguridad
* **`SWVCA-9` Modelo de Base de Datos:** Diseño relacional e implementación de la infraestructura en SQL Server mediante Code-First y Migrations de Entity Framework.
* **`SWVCA-170` Pantalla de Login Web:** Autenticación de usuarios conectada a un contexto global (`AuthContext`) para la persistencia de sesiones seguras y protección de rutas.
* **`SWVCA-190` Registro de Usuarios:** Mantenimiento (CRUD) para la gestión del personal, asignación de roles jerárquicos y control de acceso.
* **`SWVCA-182` Menú Principal Web:** Construcción del Layout principal (Sidebar y Topbar dinámicos) con renderizado condicional inteligente basado en el rol del usuario conectado.

### 📦 Módulo de Almacén
* **`SWVCA-54` Registro de Productos:** Mantenimiento del catálogo principal de mercancía, estructurado para alimentar los procesos de abastecimiento y ventas.
* **`SWVCA-117` Generar Pedido de Productos Faltantes:** Implementación del flujo maestro-detalle para las Órdenes de Compra. Incluye generación segura de correlativos (Stored Procedures), carrito en memoria y máquina de estados transaccional.

### 🤝 Módulo de Ventas & Compras
* **`SWVCA-45` Registro de Clientes:** Módulo para la captura y gestión del directorio de clientes comerciales.
* **`SWVCA-90` Registro de Proveedores:** Gestión de la base de datos de proveedores logísticos y socios estratégicos, enlazado directamente a la emisión de Órdenes de Compra.

> **Hito Técnico:** El sistema ya cuenta con control de concurrencia a nivel de base de datos, protección de rutas por roles en el Frontend y una separación limpia de capas en el Backend.
