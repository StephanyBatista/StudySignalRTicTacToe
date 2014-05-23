var proxy = null;
var roomName = null;
var isAllowToPlay = false;

$(
	function () {
		$('#SaveName').click(function () {

			$('#form-name').hide();
			StartConnectionHub();
		});
	}
);

function StartConnectionHub() {

	proxy = $.connection.ticTacToeHub;
	proxy.state.PlayerName = $('#Name').val();
	proxy.client.changeRoom = function (result) {
		CreateRooms(result);
	};
	proxy.client.startGame = function (player) {
	    startGame(player);
	};
	proxy.client.prepareMove = function (player) {
	    prepareMove(player);
	};
	proxy.client.cellMarker = function (cel, symbol) {
	    cellMarker(cel, symbol);
	};
	proxy.client.playerWinner = function (player) {
	    playerWinner(player);
	};
	proxy.client.finishedGame = function () {
	    finishedGame();
	};
	proxy.client.opponentGiveUp = function () {
	    opponentGiveUp();
	};
	
	$.connection.hub.start().done(
		function () {

			GetListRooms();
		});
}

function GetListRooms() {

	proxy.server.getListRooms().done(
		function (rooms) {

			CreateRooms(rooms);
		});
}

function CreateRooms(rooms)
{
	$('#rooms').text('');

	for (var i = 0; i < rooms.length; i++)
		CreateRoom(rooms[i]);
}

function CreateRoom(room)
{
	var div = $("<div class='col-md-2'>");
	var h2 = $("<h2>").text(room.Name);
	var p1 = $("<p>");
	if (room.PlayerOne != null)
	    p1.text(room.PlayerOne.Name + " (" + room.PlayerOne.Wins + ")");
	var p2 = $("<p>");
	if (room.PlayerTwo != null)
	    p2.text(room.PlayerTwo.Name + " (" + room.PlayerTwo.Wins + ")");
	var pAccess = $("<p>");
	if (room.PlayerOne == null || room.PlayerTwo == null)
		pAccess.append('<a class="btn btn-default" onclick="enterRoom(\'' + room.Name + '\'); return false;">Entrar</a>');

	div.append(h2);
	div.append(p1);
	div.append(p2);
	div.append(pAccess);

	$('#rooms').append(div);
}

function enterRoom(roomName) {

    this.roomName = roomName;
    proxy.state.RoomName = roomName;
    proxy.server.enterRoom(roomName);
}

function startGame(player) {
	
    $('#game').show();
    $(".move-cel").remove();
    prepareMove(player);
}

function makeMove(cel) {

    if (!isAllowToPlay) return;

    prepareToOtherPlayer();
    proxy.server.makeMove(proxy.connection.id, this.roomName, cel).done(function () {
        
    });
}

function cellMarker(cel, symbol) {

    $('#' + cel).append("<span class='move-cel'>" + symbol + "</span>");
}

function prepareMove(player) {

    if (player.Id == proxy.connection.id) {

        isAllowToPlay = true;
        $('.game-cel').css('cursor', 'pointer');
        $('#msgTime').text('Sua vez de jogar');
    } 
}

function prepareToOtherPlayer() {

    isAllowToPlay = false;
    $('.game-cel').css('cursor', 'none');
    $('#msgTime').text('Aguarde');
}

function playerWinner(player) {

    if (player.Id == proxy.connection.id)
        $('#msgGame').text('Parabéns você venceu está partida');
    else
        $('#msgGame').text('Que pena! Você perdeu');

    setTimeout(function () { $('#msgGame').text(''); }, 3500);
}

function finishedGame() {
    
    $('#msgGame').text('Jogo empatado');
}

function opponentGiveUp() {

    $('#msgGame').text('Seu adversário desistiu da partida');
    $('#game').hide();
    setTimeout(function () { $('#msgGame').text(''); }, 3500);
}



