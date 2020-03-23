$('.charity-alphabetical').change(function () {
	var n, t;
	$(this).val() != ''
		? ((n = $(this).val() == '#' ? '#\\' + this.value : '#' + this.value),
			$('.refresh-btn').show())
		: (n = '.letter-container:not(.letter-container-empty)'),
		(t = $(n)),
		$('.letter-container')
			.not(t)
			.hide(600),
		t.show(600);
});





$('.letter').each(function () {
	if ($(this).next('ul').is(':empty'))
		$(this).addClass('hiddedn');
});

