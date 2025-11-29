# PowerShell脚本：创建Config.ini文件

$configContent = @'
# ========================================
# COTUI 设备配置文件
# 创建时间: 2024-12-20
# 说明: 此文件包含所有界面和功能模块的配置参数
# ========================================

# ========================================
# MQTT 配置（MQTTForm）
# ========================================
[MQTT]
# MQTT 服务器配置
Server=192.168.1.100
Port=1883
ClientID=L1-620-004-10000012345
Username=admin
Password=123456

# MQTT 主题配置
TopicPrefix=factory/workshop1
WorkReportTopic=factory/workreport
AlarmTopic=factory/alarm
StatusTopic=factory/status

# MQTT 连接参数
QoS=2
KeepAlive=30
AutoReconnect=true
MaxReconnectAttempts=5
ReconnectInterval=5000

# MQTT 心跳
HeartbeatInterval=60000
HeartbeatTopic=factory/heartbeat


# ========================================
# 系统配置（MainPage、MainForm）
# ========================================
[System]
# 基本信息
StationName=主工位
WorkshopName=车间1
LineNo=产线1
MachineNo=L1-620-004
AssetNo=10000012345
DeviceType=检测设备

# 操作员
DefaultOperator=System
RequireOperatorLogin=true

# 系统行为
AutoStartMQTT=true
AutoStartCamera=false
EnableAutoBackup=true

# 窗口设置
MainFormTitle=COTUI 生产管理系统
MainFormWidth=1920
MainFormHeight=1080
StartMaximized=true


# ========================================
# 数据库配置（DatabaseHelper）
# ========================================
[Database]
# 数据库文件
DatabasePath=Data\ProductionData.db
DatabaseVersion=1.0

# 备份配置
AutoBackup=true
BackupTime=02:00
BackupInterval=24
BackupPath=D:\Backup\Database\
BackupKeepDays=30
BackupCompress=true

# 数据保留
DataRetentionDays=90
AutoCleanupOldData=true

# 性能配置
ConnectionPoolSize=10
CommandTimeout=30


# ========================================
# 日志配置（Logger、LogPage）
# ========================================
[Logging]
# 日志级别（Debug/Info/Warn/Error）
LogLevel=Info

# 日志文件
LogPath=Logs\
LogFileNameFormat=Log_{yyyy-MM-dd}.txt
MaxLogFileSize=10485760
MaxLogFiles=30

# 日志行为
EnableConsoleLog=true
EnableFileLog=true
EnableDatabaseLog=true
EnableEventLog=false

# 日志页面
LogPageMaxLines=1000
LogPageRefreshInterval=1000
LogPageAutoScroll=true


# ========================================
# 视觉检测配置（VisionForm）
# ========================================
[Vision]
# 相机配置
CameraIndex=0
CameraWidth=1920
CameraHeight=1080
CameraFPS=30
CameraExposure=50
CameraGain=10

# 图像保存
SaveOKImage=false
SaveNGImage=true
ImagePath=D:\ProductionImages\
ImageFormat=jpg
ImageQuality=85
ImageKeepDays=90

# 检测参数
EnableAutoInspection=true
InspectionTimeout=5000
RetryOnFail=true
MaxRetryCount=3

# ROI配置
ROI_X=100
ROI_Y=100
ROI_Width=1720
ROI_Height=880


# ========================================
# 质量参数配置（SPCMonitorPage）
# ========================================
[Quality]
# X方向尺寸
DimensionX_Name=长度
DimensionX_Unit=mm
DimensionX_Target=50.0
DimensionX_UpperLimit=50.1
DimensionX_LowerLimit=49.9
DimensionX_UpperControlLimit=50.08
DimensionX_LowerControlLimit=49.92
DimensionX_UpperWarningLimit=50.05
DimensionX_LowerWarningLimit=49.95

# Y方向尺寸
DimensionY_Name=宽度
DimensionY_Unit=mm
DimensionY_Target=30.0
DimensionY_UpperLimit=30.1
DimensionY_LowerLimit=29.9
DimensionY_UpperControlLimit=30.08
DimensionY_LowerControlLimit=29.92
DimensionY_UpperWarningLimit=30.05
DimensionY_LowerWarningLimit=29.95

# Z方向尺寸
DimensionZ_Name=高度
DimensionZ_Unit=mm
DimensionZ_Target=10.0
DimensionZ_UpperLimit=10.05
DimensionZ_LowerLimit=9.95
DimensionZ_UpperControlLimit=10.04
DimensionZ_LowerControlLimit=9.96
DimensionZ_UpperWarningLimit=10.03
DimensionZ_LowerWarningLimit=9.97

# 重量
Weight_Name=重量
Weight_Unit=g
Weight_Target=100.0
Weight_UpperLimit=102.0
Weight_LowerLimit=98.0

# SPC 参数
SPC_SampleSize=25
SPC_SubgroupSize=5
SPC_Cpk_MinValue=1.33
SPC_Ppk_MinValue=1.33


# ========================================
# 统计分析配置（StatisticsAnalysisPage）
# ========================================
[Statistics]
# 数据刷新
RefreshInterval=5000
AutoRefresh=true

# 图表配置
ChartType=Line
ChartShowLegend=true
ChartShowGrid=true
ChartPointSize=5

# 数据范围
DefaultTimeRange=24
MaxDataPoints=1000

# 导出配置
ExportPath=D:\Export\
ExportFormat=Excel
ExportIncludeChart=true


# ========================================
# 看板配置（DashboardPage）
# ========================================
[Dashboard]
# 刷新间隔
RefreshInterval=3000
AutoRefresh=true

# 显示项目
ShowProductionCount=true
ShowPassRate=true
ShowDefectRate=true
ShowEfficiency=true
ShowAlarms=true
ShowStatus=true

# 数据统计
StatisticsPeriod=Day
ShowTrend=true
TrendDays=7

# 颜色配置
PassRateGoodColor=#00FF00
PassRateWarningColor=#FFFF00
PassRateBadColor=#FF0000
PassRateWarningThreshold=95.0
PassRateBadThreshold=90.0


# ========================================
# 追溯配置（TraceabilityPage）
# ========================================
[Traceability]
# 查询配置
MaxQueryResults=1000
QueryTimeout=30000
EnableFuzzySearch=true

# 数据范围
DefaultQueryDays=30
MaxQueryDays=365

# 导出配置
ExportPath=D:\Export\Traceability\
ExportFormat=Excel
IncludeImages=true


# ========================================
# 扫码管理配置（BarcodeScanPage）
# ========================================
[Barcode]
# 扫码枪配置
ScannerType=USB
ScannerPort=COM3
ScannerBaudRate=9600
ScannerTimeout=5000

# 扫码行为
AutoTrigger=true
PlaySoundOnScan=true
PlaySoundOnError=true
RequireEnterKey=true
AutoSubmit=true

# 条码验证
MinBarcodeLength=8
MaxBarcodeLength=32
AllowDuplicate=false
BarcodePattern=^[A-Z0-9]+$

# SN 管理
SNPrefix=SN
SNLength=16
SNChecksum=true


# ========================================
# 报警配置（WarningPage）
# ========================================
[Alarm]
# 报警行为
EnableSound=true
EnableFlash=true
EnableMQTTReport=true
EnableEmail=false

# 报警级别
Level1_Name=信息
Level1_Color=#0000FF
Level2_Name=警告
Level2_Color=#FFFF00
Level3_Name=严重
Level3_Color=#FF0000

# 报警记录
MaxAlarmRecords=1000
AlarmRetentionDays=90
AutoAcknowledge=false

# 邮件配置（如果启用）
EmailServer=smtp.example.com
EmailPort=25
EmailUsername=alarm@example.com
EmailPassword=123456
EmailTo=admin@example.com


# ========================================
# 运动控制配置（MotionControlPage）
# ========================================
[Motion]
# 轴配置
AxisCount=4
Axis1_Name=X轴
Axis2_Name=Y轴
Axis3_Name=Z轴
Axis4_Name=R轴

# 速度参数
MaxSpeed=1000
DefaultSpeed=500
AccelerationTime=200
DecelerationTime=200

# 限位配置
EnableSoftLimit=true
EnableHardLimit=true
Axis1_MinPosition=0
Axis1_MaxPosition=1000
Axis2_MinPosition=0
Axis2_MaxPosition=800
Axis3_MinPosition=0
Axis3_MaxPosition=500
Axis4_MinPosition=0
Axis4_MaxPosition=360

# 控制器配置
ControllerType=EtherCAT
ControllerIP=192.168.1.10
ControllerPort=502


# ========================================
# 权限管理配置（Permission_Management）
# ========================================
[Permission]
# 用户管理
EnableUserManagement=true
RequirePasswordChange=false
PasswordMinLength=6
PasswordExpiryDays=90

# 会话管理
SessionTimeout=3600
MaxLoginAttempts=5
LockoutDuration=300

# 默认管理员
DefaultAdminUser=admin
DefaultAdminPassword=admin888

# 权限级别
Level1_Name=操作员
Level2_Name=工程师
Level3_Name=管理员
Level4_Name=超级管理员


# ========================================
# 设备校验配置（SettingForm）
# ========================================
[Calibration]
# 校验信息
LastCalibrationDate=2024-01-15
NextCalibrationDate=2025-01-15
CalibrationCycle=365
CalibrationReport=CAL-2024-001

# 校验提醒
EnableCalibrationReminder=true
ReminderDaysBefore=30

# 校验标准
CalibrationStandard=ISO-9001
CalibrationAuthority=国家计量院


# ========================================
# 界面配置（UI）
# ========================================
[UI]
# 主题
Theme=Light
ColorScheme=Blue
Language=zh-CN

# 字体
FontFamily=微软雅黑
FontSize=12
FontBold=false

# 界面行为
EnableAnimation=true
AnimationSpeed=200
ShowTooltip=true
TooltipDelay=500

# 刷新配置
UIRefreshInterval=1000
EnableProgressAnimation=true

# 快捷键
EnableHotkeys=true
Hotkey_StartProduction=F5
Hotkey_StopProduction=F6
Hotkey_EmergencyStop=F12


# ========================================
# 网络配置（Network）
# ========================================
[Network]
# 本机IP
LocalIP=192.168.1.50
LocalPort=8080

# MES 接口
MES_Enabled=true
MES_URL=http://192.168.1.200:8080/api
MES_Username=device01
MES_Password=123456
MES_Timeout=30000
MES_RetryCount=3

# ERP 接口
ERP_Enabled=false
ERP_URL=http://192.168.1.201:8080/api
ERP_Timeout=30000

# 代理配置
EnableProxy=false
ProxyServer=192.168.1.254
ProxyPort=8080


# ========================================
# 性能配置（Performance）
# ========================================
[Performance]
# 线程池
MaxWorkerThreads=10
MaxIOThreads=10

# 缓存配置
EnableCache=true
CacheSize=100
CacheDuration=3600

# 性能监控
EnablePerformanceMonitor=false
MonitorInterval=60000
MonitorLogPath=Logs\Performance\


# ========================================
# 调试配置（Debug）
# ========================================
[Debug]
# 调试模式
DebugMode=false
EnableVerboseLogging=false
EnableSQLLogging=false

# 模拟数据
EnableSimulation=false
SimulationInterval=1000
SimulationPassRate=95

# 性能分析
EnableProfiling=false
ProfilingOutputPath=Logs\Profiling\


# ========================================
# 高级配置（Advanced）
# ========================================
[Advanced]
# 并发控制
MaxConcurrentOperations=5
OperationTimeout=30000

# 错误处理
MaxErrorRetry=3
ErrorRetryDelay=1000
EnableErrorReport=true

# 数据同步
EnableDataSync=true
SyncInterval=300000
SyncServer=192.168.1.100

# 安全配置
EnableEncryption=false
EncryptionKey=YourEncryptionKey123
EnableAuditLog=true


# ========================================
# 自定义配置（Custom）
# ========================================
[Custom]
# 您可以在这里添加自定义配置
CustomParam1=Value1
CustomParam2=Value2
CustomParam3=Value3

# 工艺参数
ProcessParam1=100
ProcessParam2=200
ProcessParam3=300

# 设备特定参数
DeviceSpecificParam1=ABC
DeviceSpecificParam2=123
'@

# 写入文件
Set-Content -Path "C:\Users\18872\Desktop\CO\Config.ini" -Value $configContent -Encoding UTF8

Write-Host "? Config.ini 文件创建成功！" -ForegroundColor Green
Write-Host "文件路径: C:\Users\18872\Desktop\CO\Config.ini" -ForegroundColor Cyan
Write-Host "文件大小: $((Get-Item 'C:\Users\18872\Desktop\CO\Config.ini').Length) 字节" -ForegroundColor Cyan
Write-Host ""
Write-Host "包含的配置分组:" -ForegroundColor Yellow
Write-Host "  1. [MQTT] - MQTT通信配置" -ForegroundColor White
Write-Host "  2. [System] - 系统基本信息" -ForegroundColor White
Write-Host "  3. [Database] - 数据库配置" -ForegroundColor White
Write-Host "  4. [Logging] - 日志配置" -ForegroundColor White
Write-Host "  5. [Vision] - 视觉检测配置" -ForegroundColor White
Write-Host "  6. [Quality] - 质量参数配置" -ForegroundColor White
Write-Host "  7. [Statistics] - 统计分析配置" -ForegroundColor White
Write-Host "  8. [Dashboard] - 看板配置" -ForegroundColor White
Write-Host "  9. [Traceability] - 追溯配置" -ForegroundColor White
Write-Host "  10. [Barcode] - 扫码管理配置" -ForegroundColor White
Write-Host "  11. [Alarm] - 报警配置" -ForegroundColor White
Write-Host "  12. [Motion] - 运动控制配置" -ForegroundColor White
Write-Host "  13. [Permission] - 权限管理配置" -ForegroundColor White
Write-Host "  14. [Calibration] - 设备校验配置" -ForegroundColor White
Write-Host "  15. [UI] - 界面配置" -ForegroundColor White
Write-Host "  16. [Network] - 网络配置" -ForegroundColor White
Write-Host "  17. [Performance] - 性能配置" -ForegroundColor White
Write-Host "  18. [Debug] - 调试配置" -ForegroundColor White
Write-Host "  19. [Advanced] - 高级配置" -ForegroundColor White
Write-Host "  20. [Custom] - 自定义配置" -ForegroundColor White
Write-Host ""
Write-Host "请根据实际情况修改配置后使用！" -ForegroundColor Green
