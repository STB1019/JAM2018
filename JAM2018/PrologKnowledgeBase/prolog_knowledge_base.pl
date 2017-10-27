%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% 		JAM 2018		%
%		 Prolog			%
%	     Knowledge Base		%
%					%
%	  University of Brescia		%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


% List of JAM2018's props, with relative prop class.

%	Light (something that generates the light of the room);
%	Chest (something that contains something else, like trasures, keys or enemies);
%	Enemy (both normal and bosses);
%	Door (soemthing that is laid between a room and a tunnel going out of it);
%	Fountain;
%	Button (something on the wall that is pressed and, after that, immediately returns to the start position [delta signal]);
%	StuffThrower (something that fires out stuff: for example flamethrower, laserthrower  and so on);
%	Laser;
%	Generic power up (+1 attack, +4 defense and so on);
%	Lever (something on the wall that, if pressed, will remain in the chosen position [step signal]);
%	Statue;
%	Trap (something on the floor that you may endup be trapped into);
%	Spines;
%	Rotating blades;
%	NPC;
%	Bush;
%	Picture;
%	PC;
%	Swimming pool;
%	Tree;
%	Column;
%	Ruins;
%	Mirror;
%	Pressable platform: like a Button but on the floor;
%	Proximity sensor: something that generates an area where something may happen;
%	Barrier: a Star Trek-like barrier that goes throughout all the room and prevents you to surpass it;
%	Treasure (like an heap of gold);

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% PROPS

% Environment Props

environment(light).
environment(door).
environment(fountain).
environment(npc).
environment(bush).
environment(picture).
environment(swimming_pool).
environment(tree).
environment(column).
environment(ruins).
environment(mirror).
environment(statue).

% Activator Props

activator(light).
activator(chest).
activator(enemy).
activator(button).
activator(npc).
activator(pc).
activator(pressure_platform).
activator(barrier).
activator(proximity_sensor).
activator(lever).
activator(statue).

% Trap Props

trap(enemy).
trap(thrower).
trap(laser).
trap(spines).
trap(rotating_blades).
trap(barrier).
trap(trap).

% Reward Props

reward(chest).
%reward(door).
%reward(pc). %% TODO WUT
%reward(barrier).
reward(treasure).
reward(power_up).

% Some general properties for the Props

	% Iff the prop is the only one of its type in the maze.
isUnique(pc).
	% Iff the prop represents something alive.
isAlive(pc).
isAlive(npc).
isAlive(enemy).
	% Iff the prop is short enough to be walked on.
isWalkable(fountain).
isWalkable(thrower).
isWalkable(laser).
isWalkable(trap).
isWalkable(spines).
isWalkable(swimming_pool).
isWalkable(treasure).
isWalkable(ruins).
isWalkable(bush).
	% Iff the prop can be open/closed
canBeOpen(chest).
canBeOpen(door).
	% Iff the prop has multiple status, like "activated/deactivated".
canBeActive(P) :- activator(P).
canBeActive(P) :- trap(P).
	% Iff the prop can explode (with a lot of fire and smoke).
canExplode(chest).
canExplode(enemy).
canExplode(fountain).
canExplode(thrower).
canExplode(laser).
canExplode(statue).
canExplode(trap).
canExplode(spines).
canExplode(rotating_blades).
canExplode(npc).
canExplode(picture).
canExplode(column).
canExplode(mirror).
canExplode(pressure_platform).
canExplode(barrier).
	% Iff the prop can move and fly in the air like a fluffy cloud.
canLevitate(P) :- isAlive(P).
canLevitate(P) :- reward(P).
canLevitate(fountain).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% QUALITAS

%	OnKill: It is acticated when the attached prop HP goes to 0;
%	OnClick1: It is activated when the player use the "click 1" action on the attached prop;
%	OnClick2: It is activated when the player use the "click 2" action on the attached prop;
%	OnWalkon: It is activated when the player walks on the attached prop;
%	OnLook: It is activated when the player look at the attached prop;
%	OnCollide: It is activated when the player collide with the attached prop;
%	OnTick: It is activated every X seconds;
%
%	DoSpawn: Put the attached prop in the room;
%	DoDestroy: Put the attached prop out from the room;
%	DoOpen: Set "isOpen" of the attached prop to true;
%	DoClose: Set "isOpen" of the attached prop to false;
%	DoActivate: Set "isActive" of the attached prop to true;
%	DoDeactivate: Set "isActive" of the attached prop to false;
%	DoExplode: The attached prop is marked as "exploding";
%	DoLevitate: The attached prop will levitate from the ground by X meter;
%	DoClone: The game will create a clone of the attached prop;

% Listener Qualitas

q_list(onKill).
q_list(onClick1).
q_list(onClick2).
q_list(onWalkOn).
q_list(onLook).
q_list(onCollide).
q_list(onTick).

% Executor Qualitas

q_exec(doSpawn).
q_exec(doDestroy).
q_exec(doOpen).
q_exec(doClose).
q_exec(doActivate).
q_exec(doDeactivate).
q_exec(doExplode).
q_exec(doLevitate).
q_exec(doClone).

% Various properties for the listed qualitas

	% Iff the qualitas does not require user interaction.
pcIndipendentQualitas(onTick).
	% Iff the qualitas requires pc movements to be called.
pcMovingQualitas(onWalkOn).
pcMovingQualitas(onLook).
pcMovingQualitas(onCollide).
	% Iff the qualitas requires user interaction to be called (like mouse clicking, or key pressing).
pcInteractingQualitas(onKill). %TODO not sure if this is correct.
ocInteractingQualitas(onClick1).
ocInteractingQualitas(onClick2).
	% Iff the qualitas removes objects from the scene.
removingQualitas(doDestroy).
removingQualitas(doExplode).
	% Iff the qualitas adds objects to the scene.
addingQualitas(doSpawn).
addingQualitas(doClone).
	% Iff the qualitas moves the object.
movingQualitas(doLevitate).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% LINKS MEANINGFULNESS

% Qualitas applicability

	% Listener qualitas

isQualitasAppliable(P, onKill) :-
	isAlive(P).
isQualitasAppliable(_, onClick1) :-
	true.
isQualitasAppliable(_, onClick2) :-
	true.
isQualitasAppliable(P, onWalkOn) :-
	isWalkable(P).
isQualitasAppliable(_, onLook) :-
	true.
isQualitasAppliable(_, onCollide) :-
	true.
isQualitasAppliable(_, onTick) :-
	true. % TODO Verify

	% Executor qualitas

isQualitasAppliable(P, doSpawn) :-
	not(environment(P)),
	not(isUnique(P)).
isQualitasAppliable(P, doDestroy) :-
	not(isUnique(P)).
isQualitasAppliable(P, doOpen) :-
	canBeOpen(P).
isQualitasAppliable(P, doClose) :-
	canBeOpen(P).
isQualitasAppliable(P, doActivate) :-
	canBeActive(P).
isQualitasAppliable(P, doDeactivate) :-
	canBeActive(P).
isQualitasAppliable(P, doExplode) :-
	canExplode(P).
isQualitasAppliable(P, doLevitate) :-
	canLevitate(P).
isQualitasAppliable(P, doClone) :-
	not(isUnique(P)).

% Ordine da una qualitas verso l'altra:
% ON_qualitas --> DO_qualitas

areQualitasOrdered(Q1, Q2) :-
	q_list(Q1),
	q_exec(Q2).

% I due Prop hanno associate delle qualitas sensate.

areQualitasCorrect(P1, Q1, P2, Q2) :-
	areQualitasOrdered(Q1, Q2),
	isQualitasAppliable(P1, Q1),
	isQualitasAppliable(P2, Q2).

% Definisco i collegamenti sensati.
% P1 (con qualitas Q1) sarà il primo prop considerato, e la "freccia" punterà verso P2 (con qualitas Q2).

isLinkMeaningful(P1, Q1, P2, Q2) :-
	areQualitasCorrect(P1, Q1, P2, Q2),
	not(isLinkStupid(P1, Q1, P2, Q2)).

%	isLinkMeaningful(P1, Q1, P2, Q2) :-
%		areQualitasCorrect(P1, Q1, P2, Q2).
%		% ET CETERAAAAAAA
	% TODO

% Definisco i collegamenti insensati.

isLinkStupid(_, Q1, _, Q2) :-
	pcIndipendentQualitas(Q1),
	removingQualitas(Q2).
	% TODO




























