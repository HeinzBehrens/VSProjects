Imports System.Threading
Imports HBE_API_2013

Public Class Form1

    ''' <summary>
    ''' Diese Klasse verwende ich, um einen Hintergrundporzess zu steuern
    ''' Aktuell wurde testroutine über einen Timer aktiviert...
    ''' Dies kann folgendermassen aussehen:
    '''        ' jetzt sagen wir ihm, was er zu tun hat
    '''        timerFeeder = New System.Timers.Timer()
    '''
    '''         AddHandler() timerFeeder.Elapsed, AddressOf testcl.testroutine
    '''     timerFeeder.SynchronizingObject = Me
    '''     timerFeeder.Interval = 1 * 1000
    '''     timerFeeder.AutoReset = True
    '''     timerFeeder.Start()
    ''' 
    ''' </summary>
    Private Class rtbxInfos

        ' ein RichTextBoxControl, damit ich Infos hineinschreiben kann
        '
        Dim _ctrlInfo As clHbeControl

        Public zaehler As Integer = 1
        Sub testroutine()
            _ctrlInfo.AppendTextNL(zaehler.ToString("00") & " Dies ist ein Test aus der Testroutine")
            zaehler += 1
        End Sub

        ''' <summary>
        ''' Initilisierung
        ''' </summary>
        ''' <param name="ctrlinfo">RichTextBox um Infos hineinzuschreiben</param>
        Sub New(ctrlinfo As clHbeControl)
            _ctrlInfo = ctrlinfo
        End Sub

    End Class



    Dim infoBox As clHbeControl
    Public timerFeeder As System.Timers.Timer
    Private testcl As rtbxInfos
    Dim StartPort As Integer = 54444
    Dim AktPort As Integer = 0


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        infoBox = New clHbeControl(Me.rtbxInfo)
        infoBox.SetText("Listener Programm gestartet")

        ' nun meine Testklasse einrichten
        testcl = New rtbxInfos(infoBox)


        ' Timer starten mit Testroutine

        ' jetzt sagen wir ihm, was er zu tun hat
        timerFeeder = New System.Timers.Timer()

        AddHandler timerFeeder.Elapsed, AddressOf testcl.testroutine
        timerFeeder.SynchronizingObject = Me
        timerFeeder.Interval = 1 * 1000
        timerFeeder.AutoReset = True
        timerFeeder.Start()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Dim myThread As New Thread(AddressOf testcl.testroutine)

        myThread.Start()

    End Sub

    Private Sub btnStartListener_Click(sender As Object, e As EventArgs) Handles btnStartListener.Click
        For i = 0 To 10
            AktPort = StartPort + i
            If isTcpPortAvailable(AktPort) Then

                infoBox.AppendTextNL("Port: " & AktPort & " ist verfügbar!")
                Exit For


            End If
        Next

    End Sub
End Class
